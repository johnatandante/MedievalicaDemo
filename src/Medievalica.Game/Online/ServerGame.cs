using Medievalica.Game.Utils.Commands;
using Medievalica.Game.Utils.Events;
using Medievalica.Game.Utils.Results;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Interfaces;
using Medievalica.Game.Utils.Messages;

namespace Medievalica.Game.Online
{
    public class ServerGame
    {

        Dictionary<string, WebSocket> sessions = new Dictionary<string, WebSocket>();
        List<ISessionClient> sessionClients = new List<ISessionClient>();

        Queue<IMessage> QueuedMessages = new Queue<IMessage>();

        ISessionClient[] Clients => sessionClients.ToArray();

        static ServerGame _Instance;

        public static ServerGame Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ServerGame();

                return _Instance;
            }
        }

        public async Task HandleRequest(WebSocket webSocket)
        {

            byte[] buffer = new byte[1024 * 4];

            ISessionClient client = null;

            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                client = await HandleCommand(buffer, result, webSocket);

                buffer = new byte[1024 * 8];
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

            if (client != null)
            {
                sessions.Remove(client.TokenId);
                sessionClients.RemoveAll(c => c.TokenId == client.TokenId);
            }

        }

        public async Task NotifyClients(WebSocket webSocket)
        {
            
            while (!webSocket.CloseStatus.HasValue)
            {
                while (QueuedMessages.Any())
                {

                    var buffer = HandleMessage(QueuedMessages.Dequeue(), webSocket);

                    // stream to all clients
                    foreach (string id in Clients.Select(c => c.TokenId))
                        await sessions[id].SendAsync(new ArraySegment<byte>(buffer),
                            WebSocketMessageType.Binary, true, CancellationToken.None);

                }

                Thread.Sleep(450);
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
            
        }

        private byte[] HandleMessage(IMessage message, WebSocket webSocket)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));   
        }

        private async Task<ISessionClient> HandleCommand(byte[] buffer, WebSocketReceiveResult result, WebSocket webSocket)
        {
            ISessionClient sessionClient = null;
            ResultCommand resultCommand = null;

            SimpleCommand command =
                   JsonConvert.DeserializeObject<SimpleCommand>(Encoding.UTF8.GetString(buffer));

            switch (command.Command)
            {
                case CommandFactory.LoginCommand:

                    sessionClient = OnlineClient.GetNew(command.Data as string);
                    sessionClients.Add(sessionClient);

                    resultCommand = ResultCommandFactory.LoggedIn(command.TokenId);

                    // memorizzo il token
                    if (!sessions.ContainsKey(sessionClient.TokenId))
                        sessions.Add(sessionClient.TokenId, webSocket);

                    buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resultCommand));

                    // send back with login id
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer),
                        result.MessageType, result.EndOfMessage, CancellationToken.None);

                    break;
                case CommandFactory.MessageCommand:
                    sessionClient = sessionClients.FirstOrDefault(c => c.TokenId == command.TokenId);
                    if (sessionClient != null)
                    {
                        QueuedMessages.Enqueue(MessageFactory.GetNew(command.Data.ToString()));
                    }
                    else
                    {
                        resultCommand = ResultCommandFactory.Failed(command.TokenId);
                    }

                    break;
                default:
                    break;

            }

            return sessionClient;
        }
    }
}

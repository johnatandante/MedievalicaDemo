using System;
using System.Linq;
using System.Collections.Generic;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Utils;
using Medievalica.Game.Utils.Commands;
using Medievalica.Game.Utils.Events;
using Medievalica.Game.Utils.Results;

namespace Medievalica.Game.Online
{
    public class OnlineGame : IGame
    {
       
        static OnlineGame _Instance;

        IGameClient Client { get; set; } 

        public static OnlineGame Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new OnlineGame();

                return _Instance;
            }
        }

        public string[] Rooms => throw new NotImplementedException();

        public bool Online => socket.Connected;

        WebSoketGameClient socket;

        PushWebSoketGameClient pushSocket;

        CommandFactory Factory;

        public OnlineGame() {

            Factory = new CommandFactory();

            socket = new WebSoketGameClient();
            pushSocket = new PushWebSoketGameClient();

            pushSocket.OnMessageReady += PushSocket_OnMessageReady;
            
        }

        private void PushSocket_OnMessageReady(object sender, DataReadyEventArgs args)
        {
            IMessage message = args.Result as IMessage;
            if (message == null)
                return;

            Client.DisplayMessage(message.TimeStamp.ToString() + " - " + message.Data);
        }

        public async Task<string> Connect(IGameClient client)
        {
            Client = client;
            Factory.SetClientId(Client.TokenId);

            await socket.Connect();
            
            await socket.Send(Factory.Login());

            ResultCommand result = await socket.Receive<ResultCommand>();
            
            //if(result != null && result.Data is string)
            //{
            //    Task.Run( () => pushSocket.ListenToStream() );
            //}

            return result != null ? result.Data as string : string.Empty;

        }
        
        public async Task StreamMessage(string message)
        {
            if (!socket.Connected)
                return;
            
            await socket.Send(Factory.NewMessage(message));

        }

        public async Task Disconnect(IGameClient client)
        {
            if (!socket.Connected)
                return;

            await StreamMessage(client.Name + " has left the online game");
            await socket.Disconnect();

        }

        public async Task<IGameRoom> GetRoom(string room)
        {
            if (!socket.Connected)
                return default(IGameRoom);

            var command = Factory.Query("queryroom", room);
            await socket.Send(command);

            ResultCommand result = await socket.Receive<ResultCommand>();

            return result != null ? result.Data as IGameRoom : default(IGameRoom);

        }
    }
}


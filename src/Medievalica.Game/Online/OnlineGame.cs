using System;
using System.Linq;
using System.Collections.Generic;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Utils;
using Medievalica.Game.Utils.Commands;
using Medievalica.Game.Utils.Events;

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

        public OnlineGame() {

            socket = new WebSoketGameClient();

        }

        public async Task<string> Connect(IGameClient client)
        {
            Client = client;

            await socket.Connect();
            
            await socket.Send(CommandController.Login);

            IResultCommand result = await socket.Receive<IResultCommand>();
        
            return result != null ? result.TokenId : string.Empty;

        }

        public async Task StreamMessage(string message)
        {
            if (!socket.Connected)
                return;
            
            await socket.Send(CommandController.NewMessage(message));

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

            var command = CommandController.Query("queryroom", room);
            await socket.Send(command);

            IResultCommand result = await socket.Receive<IResultCommand>();

            return result != null ? result.Data as IGameRoom : default(IGameRoom);

        }
    }
}


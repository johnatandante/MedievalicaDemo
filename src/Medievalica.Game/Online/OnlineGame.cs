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

        WebSoketGameClient socket;

        public OnlineGame() {

        }

        public async Task<string> Connect(IGameClient client)
        {
            Client = client;

            socket = new WebSoketGameClient();

            var command = CommandController.GetNewSimpleCommand("login");
           // socket.OnDataReady += ReadDataFromSocket;

            await socket.Send(command);

            return await socket.ReceiveJson();
        }

        public async Task StreamMessage(string message)
        {
            if (!socket.Connected)
                return;

            var command = CommandController.GetNewMessageWithData("message", message);

            await socket.Send(command);

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

            var command = CommandController.GetNewQueryCommand("queryroom", room);
            await socket.Send(command);

            return await socket.Receive<IGameRoom>();

        }
    }
}


using System;
using System.Linq;
using System.Collections.Generic;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Utils;
using Medievalica.Game.Utils.Commands;

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
            command.OnDataReady += GetLoginToken;
            await socket.Send(command);

        }

        private void GetLoginToken(object sender, DataReadyEventArgs args)
        {
            Client.SetTokenId(args.Result.Data.ToString());

        }

        public async Task StreamMessage(string message)
        {
            //foreach (IGameClient client in clients)
            //    await client.DisplayMessage(message);
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async Task Disconnect(IGameClient client)
        {
            await StreamMessage(client.Name + " has left the online game");
            await socket.Disconnect();

        }

        public IGameRoom GetRoom(string room)
        {
            throw new NotImplementedException();
        }
    }
}


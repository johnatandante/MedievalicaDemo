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

        List<IGameClient> clients => throw new NotImplementedException();

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
            socket = new WebSoketGameClient();
            
            await Task.Delay(1);
            socket.Send(CommandController.GetNewSimpleCommand("login"));

            throw new NotImplementedException();
            //clients.Add(client);

            //await StreamMessage(string.Format("{0} has joined the game", client.Name));

            //return Guid.NewGuid().ToString();

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
            await Task.Delay(1);
            throw new NotImplementedException();

        }

        public IGameRoom GetRoom(string room)
        {
            throw new NotImplementedException();
        }
    }
}


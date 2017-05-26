using System;
using System.Linq;
using System.Collections.Generic;
using Medievalica.Game.Interfaces;
using System.Threading.Tasks;
using Medievalica.Game.Controllers;

namespace Medievalica.Game
{
    public class MainGame : IGame
    {

        static Dictionary<string, IGameRoom> GameRooms = new Dictionary<string, IGameRoom>();

        static MainGame _Instance;

        List<IGameClient> clients = new List<IGameClient>();

        public static IGame Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new MainGame();

                return _Instance;
            }
        }

        public string[] Rooms => GameRooms.Keys.ToArray();

        public bool Online => false;

        public MainGame() { }

        public async Task<string> Connect(IGameClient client)
        {
            await Task.Delay(1);
            clients.Add(client);

            await StreamMessage(string.Format("{0} has joined the game", client.Name));

            return Guid.NewGuid().ToString();

        }

        public async Task StreamMessage(string message)
        {
            foreach (IGameClient client in clients)
                await client.DisplayMessage(message);
        }

        public async Task Disconnect(IGameClient client)
        {
            await Task.Delay(1);

            await StreamMessage(string.Format("{0} has left the game", client.Name));

            clients.Remove(client);

        }

        public async Task<IGameRoom> GetRoom(string room)
        {
            await Task.Delay(1);
            if (!GameRooms.ContainsKey(room))
                GameRooms.Add(room, GameRoomController.GetNew(room));

            return GameRooms[room];
        }

    }
}

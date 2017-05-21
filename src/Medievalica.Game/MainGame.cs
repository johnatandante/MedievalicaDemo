using System;
using System.Collections.Generic;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game
{
    public class MainGame
    {

        static MainGame _Instance;

        public List<IGameClient> clients = new List<IGameClient>();

        public static MainGame Instance {
            get{
                if(_Instance == null)
                    _Instance = new MainGame();

                return _Instance;
            }
        }
        
        public MainGame() { }

        public async Task Connect(IGameClient client){
            await Task.Delay(1);
            clients.Add(client);

        }

        public async Task Disconnect(IGameClient client){
            await Task.Delay(1);
            clients.Remove(client);

        }

    }
}

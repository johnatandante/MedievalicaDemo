using Medievalica.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Medievalica.Game.Rooms
{
    public class ChatRoom : IChatRoom
    {
        public IGameClient[] Clients => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public Task Join(IGameClient client)
        {
            throw new NotImplementedException();
        }

        public Task Leave(IGameClient client)
        {
            throw new NotImplementedException();
        }

        public Task StreamMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}

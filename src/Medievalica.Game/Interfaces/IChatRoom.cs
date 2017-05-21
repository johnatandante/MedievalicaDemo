using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface IChatRoom {

        IGameClient[] Clients { get; }

        Task Join(IGameClient client);

        Task Leave(IGameClient client);

        Task StreamMessage(string message);

    }
}
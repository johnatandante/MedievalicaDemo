using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    
    public interface IGameClient {

        Task Connect();
        Task Disconnect();

        Task Join(IGameRoom room);
        Task Exit(IGameRoom room);

        Task Join(IChatRoom room);
        Task Exit(IChatRoom room);

    }
}
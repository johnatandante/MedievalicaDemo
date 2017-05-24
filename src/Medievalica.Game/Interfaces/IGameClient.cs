using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    
    public interface IGameClient {

        string Name { get; }

        Task Connect();
        Task Disconnect();

        Task Join(IGameRoom room);
        Task Exit(IGameRoom room);

        Task Join(IChatRoom room);
        Task Exit(IChatRoom room);

        Task DisplayMessage(string message, ICharacter character);

        Task DisplayMessage(string message);

    }
}
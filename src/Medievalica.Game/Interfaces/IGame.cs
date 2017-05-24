
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface IGame
    {
        string[] Rooms { get; }

        Task<string> Connect(IGameClient client);

        Task StreamMessage(string message);

        Task Disconnect(IGameClient client);

        IGameRoom GetRoom(string room);

    }
}
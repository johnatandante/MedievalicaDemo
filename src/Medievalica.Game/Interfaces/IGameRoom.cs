using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface IGameRoom {

       ICharacter[] CharacterList { get; }

    }
}
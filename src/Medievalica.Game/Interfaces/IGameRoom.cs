using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface IGameRoom {

       ICharacter[] CharacterList { get; }

        IEquipment[] EquipmentList { get; }
       
        string Name { get; }

        Task Join(ICharacter client);

        Task Leave(ICharacter client);

        Task StreamMessage(string message);

        Task Add(IEquipment[] equipments);
    }
}
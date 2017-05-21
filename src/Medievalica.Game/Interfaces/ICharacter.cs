using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface ICharacter : ISkilled {

       string NickName { get; }

       // Personal Info Character Data
       //int Age { get; }
       //int Weight { get; }
       //int Height { get; }
       // HairColor { get; }
       // EyeColor { get; }
       // SkinColor { get; }

       int Stamina { get; }

       IEquipment[] Equipment { get; }

       void Attack(ICharacter opponent);

       void DefenceFrom(ISkilled opponent);

       void EquipWith(IEquipment item);

       void Remove(IEquipment item);

    }
}
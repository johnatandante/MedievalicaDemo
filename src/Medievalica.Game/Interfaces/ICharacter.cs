using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface ICharacter : IItemWithSkill
    {

        string NickName { get; }

        // Personal Info Character Data
        //int Age { get; }
        //int Weight { get; }
        //int Height { get; }
        // HairColor { get; }
        // EyeColor { get; }
        // SkinColor { get; }

        float Stamina { get; }

        IEquipment[] Equipment { get; }

        void AttackTo(ICharacter opponent);

        float DefenceFrom(IItemWithSkill opponent);

        void EquipWith(IEquipment item);

        void Remove(IEquipment item);

        void SendMessage(string message);

        bool IsAlive { get; }

    }
}
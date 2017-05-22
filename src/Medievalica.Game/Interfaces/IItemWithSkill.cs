using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{

    public interface IItemWithSkill
    {
        float Precision { get; }
        float Attack { get; }
        float Defence { get; }

        float AttackPower { get; }
        float DefencePower { get; }

        void SetSkill(float precision, float attack, float defence);
    }

}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{

    public interface ISkilled
    {
       int Precision { get; }
       int AttackPower { get; }
       int DefencePower { get; }

    }

}
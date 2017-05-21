using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface IEquipment : ISkilled {

       string Name { get; }
       
       // SkinColor { get; }

    }
}
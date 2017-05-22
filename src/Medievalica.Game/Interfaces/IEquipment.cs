using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Interfaces
{
    public interface IEquipment : IItemWithSkill {

       string Name { get; }
       
       // SkinColor { get; }

    }
}
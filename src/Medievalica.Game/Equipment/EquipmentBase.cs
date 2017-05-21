using System;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Equipment
{
    public abstract class EquipmentBase : IEquipment {

       public abstract string Name { get; }

       public int Precision { get; protected set; }
       public int AttackPower { get; protected set; }
       public int DefencePower { get; protected set; }

       public void SetSkill(int precision, int attack, int defence){
           Precision = precision;
           AttackPower = attack;
           DefencePower = defence;

       }

    }
}
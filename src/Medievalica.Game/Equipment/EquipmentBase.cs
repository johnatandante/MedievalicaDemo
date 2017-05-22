using System;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Equipment
{
    public abstract class EquipmentBase : IEquipment {

       public abstract string Name { get; }

       public float Precision { get; protected set; }
       public float Attack { get; protected set; }
       public float Defence { get; protected set; }

        public float AttackPower => Precision * AttackPower;

        public float DefencePower => Precision * Defence;

        public void SetSkill(float precision, float attack, float defence){
           Precision = precision;
           Attack = attack;
           Defence = defence;

       }

    }
}
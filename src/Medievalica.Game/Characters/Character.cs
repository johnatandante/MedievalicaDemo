using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Interfaces;

namespace Medievalica.Game.Characters
{
    public class Character : ICharacter {

        IGameClient parent;

        bool HasParent => parent != null;

       public string NickName { get; protected set; }

       // Personal Info Character Data
       //int Age { get; }
       //int Weight { get; }
       //int Height { get; }
       // HairColor { get; }
       // EyeColor { get; }
       // SkinColor { get; }

       public float Precision { get; protected set; }
       public float Attack { get; protected set; }
       public float Defence { get; protected set; }
        
        public float AttackPower
        {
            get { return Attack * Precision; }
        }

        public float DefencePower
        {
            get { return Defence * Precision; }
        }

        public float Stamina { get; protected set; }

        public bool IsAlive => Stamina > 0f;

        List<IEquipment> _Equipment = new List<IEquipment>();
        public IEquipment[] Equipment { 
           get {
             return _Equipment.ToArray();
            } 
        }

       public Character(IGameClient client = null) {
           NickName = "Character_" + DateTime.Now.Ticks.ToString();
            parent = client;
       }

       public void SetName(string name) {
           NickName = name;
       }

       public void SetSkill(float precision, float attack, float defence){
           Precision = precision;
           Attack = attack;
           Defence = defence;

       }

       public void EquipWith(IEquipment item){
           _Equipment.Add(item);
       }

       public void Remove(IEquipment item){
           _Equipment.Remove(item);
       }

       public void AttackTo(ICharacter opponent){
            //
            Stamina = Math.Min(Stamina - opponent.DefenceFrom(this), 0);
       }

       public float DefenceFrom(IItemWithSkill opponent){
            //
            return Math.Max(opponent.AttackPower - this.DefencePower, 0);
       }

        public void SendMessage(string message)
        {
            if(HasParent)
                parent.DisplayMessage(message, this);
            
        }
    }
}
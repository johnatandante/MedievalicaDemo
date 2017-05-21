using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Interfaces;

namespace Medievalica.Game.Characters
{
    public class Character : ICharacter {

       public string NickName { get; protected set; }

       // Personal Info Character Data
       //int Age { get; }
       //int Weight { get; }
       //int Height { get; }
       // HairColor { get; }
       // EyeColor { get; }
       // SkinColor { get; }

       public int Precision { get; protected set; }
       public int AttackPower { get; protected set; }
       public int DefencePower { get; protected set; }

       public int Stamina { get; protected set; }

       List<IEquipment> _Equipment;
       public IEquipment[] Equipment { 
           get {
             return _Equipment.ToArray();
            } 
        }

       public Character() {
           NickName = "Character_" + DateTime.Now.Ticks.ToString();
           _Equipment = new List<IEquipment>();
       }

       public void SetName(string name) {
           NickName = name;
       }

       public void SetSkill(int precision, int attack, int defence){
           Precision = precision;
           AttackPower = attack;
           DefencePower = defence;

       }

       public void EquipWith(IEquipment item){
           _Equipment.Add(item);
       }

       public void Remove(IEquipment item){
           _Equipment.Remove(item);
       }

       public void Attack(ICharacter opponent){

       }

       public void DefenceFrom(ISkilled opponent){

       }

    }
}
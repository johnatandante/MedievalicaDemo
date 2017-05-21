using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Equipment
{
    public class Axe : EquipmentBase {

       public override sealed string Name { 
           get {
            return "Axe";
            } 
       }

       public Axe() {
           SetSkill(5, 10, 2);

       }

    }
}
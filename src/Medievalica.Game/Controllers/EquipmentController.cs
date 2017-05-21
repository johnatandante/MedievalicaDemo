using System;
using Medievalica.Game.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Equipment;

namespace Medievalica.Game.Controllers
{
    public class EquipmentController
    {

        public static async Task<IEquipment> GetNewAxe() {
            
            // mocked
            await Task.Delay(1);
            return new Axe();

        }
    }
}
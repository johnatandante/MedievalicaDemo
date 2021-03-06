using System;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Interfaces;
using Medievalica.Game.Characters;

namespace Medievalica.Game.Controllers
{
    public class CharacterController
    {

        public static async Task<ICharacter> LoadCharacterProfile(IGameClient client){
            
            // await mocked data
            await Task.Delay(1);

            // Mocked  
            Character character = new Character(client);

            character.SetName("Pippo");
            character.EquipWith(await EquipmentController.GetNewAxe());
            
            return character;

        }

    }
}
using Medievalica.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Medievalica.Game.Rooms
{
    public class GameRoom : IGameRoom
    {

        private List<ICharacter> _CharacterList = new List<ICharacter>();

        public ICharacter[] CharacterList => _CharacterList.ToArray();

        public string Name { get; set; }

        private List<IEquipment> _EquipmentList = new List<IEquipment>();

        public IEquipment[] EquipmentList => _EquipmentList.ToArray();

        public async Task Add(IEquipment[] equipments)
        {
            await Task.Delay(1);
           _EquipmentList.AddRange(equipments);

        }

        public async Task Join(ICharacter character)
        {
            await Task.Delay(1);
            _CharacterList.Add(character);
            character.SendMessage("You joined room " + Name + " with " + EquipmentList.Length + " equipment to find and " + (CharacterList.Length - 1) + " other players");
        }

        public async Task Leave(ICharacter character)
        {
            await Task.Delay(1);
            _CharacterList.Remove(character);
            character.SendMessage("You left room " + Name);
        }

        public async Task StreamMessage(string message)
        {
            await Task.Delay(1);
            foreach (ICharacter character in _CharacterList)
                character.SendMessage(message);

        }



    }
}

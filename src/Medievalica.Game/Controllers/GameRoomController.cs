using System;
using System.Collections.Generic;
using System.Text;
using Medievalica.Game.Interfaces;
using Medievalica.Game.Rooms;

namespace Medievalica.Game.Controllers
{
    public class GameRoomController
    {

        static Dictionary<string, IGameRoom> GameRooms = new Dictionary<string, IGameRoom>();

        public static IGameRoom GetRoom(string room)
        {
            if (!GameRooms.ContainsKey(room))
                GameRooms.Add(room, GetNew(room));

            return GameRooms[room];

        }

        private static IGameRoom GetNew(string room)
        {
            return new GameRoom() { Name = room };
        }
    }
}

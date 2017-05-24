using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Medievalica.Game.Interfaces;
using Medievalica.Game.Rooms;

namespace Medievalica.Game.Controllers
{
    public class GameRoomController
    {

        public static IGameRoom GetNew(string room)
        {
            return new GameRoom() { Name = room };
        }

    }
}

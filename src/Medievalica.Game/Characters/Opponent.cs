using System;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Characters
{
    public class Opponent : Character {

        public Opponent(){
            NickName = "Opponent_" + DateTime.Now.Ticks.ToString();
        }

    }
}
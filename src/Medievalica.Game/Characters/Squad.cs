using System;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game.Interfaces;

namespace Medievalica.Game.Characters
{
    public class Squad : ISquad {

        public string Name { get; }

        public Squad(string name){
            this.Name = name;

        }

    }
}
using System;
using Medievalica.Game.Utils.Interfaces;

namespace Medievalica.Game.Utils.Commands
{

    internal class SimpleCommand : ICommand
    {
        private string commandName;

        public SimpleCommand(string commandName)
        {
            this.commandName = commandName;
        }

        public string Command => commandName;

    }
}
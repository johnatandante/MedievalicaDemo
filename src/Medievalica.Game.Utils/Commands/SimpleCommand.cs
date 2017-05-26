using System;
using Medievalica.Game.Utils.Events;

namespace Medievalica.Game.Utils.Commands
{

    internal class SimpleCommand : ICommand
    {
        private string commandName;

        public SimpleCommand(string commandName)
        {
            this.commandName = commandName;
            TokenId = CommandGameHelper.GetNewTokenId();
        }

        public string Command => commandName;

        public string TokenId { get; private set; }
    }
}
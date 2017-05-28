using System;
using Medievalica.Game.Utils.Events;

namespace Medievalica.Game.Utils.Commands
{

    public class SimpleCommand : ICommand
    {

        public SimpleCommand(string commandName)
        {
            Command = commandName;
            TokenId = CommandGameHelper.GetNewTokenId();
        }

        public string Command { get; set; }

        public string TokenId { get; set; }

        public object Data { get; set; }

    }
}
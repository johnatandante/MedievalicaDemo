using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
   internal class Command : SimpleCommand
    {
        public string Message { get; private set; }

        public Command( string commandName, string message)
            : base(commandName)
        {
            Message = message;
        }

    }
}

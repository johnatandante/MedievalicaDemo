using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    public class Command : SimpleCommand
    {

        public string Message => Data as string;

        public Command( string commandName, string message)
            : base(commandName)
        {
            Data = message;
        }

    }
}

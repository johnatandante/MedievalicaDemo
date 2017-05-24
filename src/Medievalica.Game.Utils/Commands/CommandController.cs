using Medievalica.Game.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    public class CommandController
    {
        public static ICommand GetNewSimpleCommand(string commandName)
        {
            return new SimpleCommand(commandName);

        }


    }
}

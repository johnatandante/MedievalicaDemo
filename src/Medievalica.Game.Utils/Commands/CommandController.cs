
using Medievalica.Game.Utils.Events;
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

        public static ICommand GetNewMessageWithData(string commandName, string message)
        {
            return new Command(commandName, message);
        }


        public static ICommand GetNewQueryCommand(string commandName, params string[] args)
        {
            return new QueryCommand(commandName, args);
        }

    }
}

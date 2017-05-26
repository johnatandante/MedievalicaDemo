
using Medievalica.Game.Utils.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    public class CommandController
    {
        public const string LoginCommand = "login";

        public const string MessageCommand = "message";

        public static ICommand Simple(string commandName)
        {
            return new SimpleCommand(commandName);

        }

        public static ICommand Login
        {
            get
            {
                return new SimpleCommand(LoginCommand);
            }

        }

        public static ICommand NewMessage(string message)
        {
            return new Command(MessageCommand, message);
        }


        public static ICommand Query(string commandName, params string[] args)
        {
            return new QueryCommand(commandName, args);
        }

    }
}

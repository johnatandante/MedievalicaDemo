
using Medievalica.Game.Utils.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    public class CommandFactory
    {
        public const string LoginCommand = "login";

        public const string MessageCommand = "message";
        
        string ClientId = string.Empty;

        public CommandFactory() { }

        public void SetClientId(string clientId)
        {
            ClientId = clientId;
        }

        public ICommand Simple(string commandName)
        {
            return new SimpleCommand(commandName);
        }

        public ICommand Login()
        {
            return new SimpleCommand(LoginCommand) { Data = ClientId };
        }

        public ICommand NewMessage(string message)
        {
            return new SimpleCommand(MessageCommand) { TokenId = ClientId, Data = message };
        }
        
        public ICommand Query(string commandName, params string[] args)
        {
            return new SimpleCommand(commandName) { TokenId = ClientId, Data = args };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    public class QueryCommand : SimpleCommand
    {

        public string[] QueryArgs => Data as string[];

        public QueryCommand(string commandName, params string[] queryArgs)
            : base(commandName)
        {
            Data = queryArgs ?? new string[] { };

        }
    }
}

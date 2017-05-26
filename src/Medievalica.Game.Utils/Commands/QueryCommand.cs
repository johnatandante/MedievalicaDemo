using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    class QueryCommand : SimpleCommand
    {

        public string[] QueryArgs { get; private set; }

        public QueryCommand(string commandName, params string[] queryArgs)
            : base(commandName)
        {
            QueryArgs = queryArgs;

        }
    }
}

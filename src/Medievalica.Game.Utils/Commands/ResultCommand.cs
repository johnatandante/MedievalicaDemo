using Medievalica.Game.Utils.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Commands
{
    public class ResultCommand : IResultCommand
    {
        public object Data { get; private set; }

        public string TokenId { get; private set; }

        public ResultCommand(string id, object result)
        {
            TokenId = id;
            Data = result;
        }
    }
}

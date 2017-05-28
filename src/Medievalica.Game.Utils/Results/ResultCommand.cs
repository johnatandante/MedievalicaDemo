using Medievalica.Game.Utils.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Results
{
    public class ResultCommand : IResultCommand
    {
        public object Data { get; set; }

        public string TokenId { get; set; }

        public ResultCommand()
        {

        }
    }
}

using Medievalica.Game.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Events
{

    public class DataReadyEventArgs : EventArgs
    {
        public IResultCommand Result { get; private set; }

        public DataReadyEventArgs(IResultCommand command)
        {
            Result = command;

        }

    }
}

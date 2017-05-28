
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Events
{

    public class DataReadyEventArgs : EventArgs
    {
        public object Result { get; private set; }

        public DataReadyEventArgs(object message)
        {
            Result = message;

        }

    }
}

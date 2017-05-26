using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Events
{
    public class CommandGameHelper
    {

        public delegate void DataReadDelegate(object sender, DataReadyEventArgs args);

        internal static string GetNewTokenId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

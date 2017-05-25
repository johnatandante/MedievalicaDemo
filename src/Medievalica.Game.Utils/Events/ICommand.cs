using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Events
{
    public interface ICommand
    {
        string Command { get; }

        event CommandGameHelper.DataReadDelegate OnDataReady;

    }
}

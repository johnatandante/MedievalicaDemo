using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Events
{
    public interface IResultCommand
    {
        string TokenId { get; }

        object Data { get; }

    }
}

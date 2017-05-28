using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Events
{
    public interface IMessage
    {
        string Data { get; }

        string TokenId { get; }

        DateTime TimeStamp { get; }

    }
}

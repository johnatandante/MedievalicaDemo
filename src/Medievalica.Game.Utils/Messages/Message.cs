using Medievalica.Game.Utils.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Messages
{
    public class Message : IMessage
    {
        public string Data { get; set; }

        public string TokenId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}

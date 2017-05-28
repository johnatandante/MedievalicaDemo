using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Messages
{
    public class MessageFactory
    {

        public static Message GetNew(string message)
        {
            return new Message() { Data = message, TimeStamp = DateTime.Now };
        }
    }
}

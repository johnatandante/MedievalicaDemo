using Medievalica.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Medievalica.Game.Online
{
    public class OnlineClient : ISessionClient
    {
        public string Name => TokenId;

        public string TokenId { get; set; }
        
        public static ISessionClient GetNew(string id)
        {
            return new OnlineClient() { TokenId = id };
        }

    }
}

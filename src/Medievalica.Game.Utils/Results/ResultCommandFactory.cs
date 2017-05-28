using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Utils.Results
{
    public class ResultCommandFactory
    {

        public const string LoggedInResult = "LoggedIn";
        
        public const string FailedResult = "LoggedIn";

        public static ResultCommand LoggedIn(string tokenId)
        {
            return new LoggedInCommand() { TokenId = tokenId };
        }

        public static ResultCommand Failed(string tokenId)
        {
            return new ResultCommand() { TokenId = tokenId, Data = "error" };
        }

    }
}

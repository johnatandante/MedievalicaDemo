using Medievalica.Game.Utils.Events;

namespace Medievalica.Game.Utils.Results
{
    public class LoggedInCommand : ResultCommand
    {
        public LoggedInCommand() 
        {

            Data = CommandGameHelper.GetNewTokenId();

        }
    }
}
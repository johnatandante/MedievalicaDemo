using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game;
using Medievalica.Game.Interfaces;
using Medievalica.Game.Controllers;
using Medievalica.Game.Online;

namespace Medievalica.Client {

    public class ConsoleClient : IGameClient {

        public string Name { get; private set; }

        public string TokenId { get; private set; }

        IGame CurrentGame { get; set; }
        ICharacter  mainCharacter;

        string currentCommand = string.Empty;

        public ConsoleClient(){
            CurrentGame = MainGame.Instance;
            
            mainCharacter = 
             CharacterController
             .LoadCharacterProfile(this)
             .GetAwaiter().GetResult();

        }

        public void SetName(string name)
        {
            Name = name;

        }

        public async Task Connect(){
            TokenId = await CurrentGame.Connect(this);
            
        }

        public  async Task Disconnect(){

            await CurrentGame.Disconnect(this);
            TokenId = string.Empty;
        }

        public async Task Join(IGameRoom room){
            await Task.Delay(1);
            await room.Join(mainCharacter);
        }

        public async Task Exit(IGameRoom room)
        {
            await Task.Delay(1);
            await room.Leave(mainCharacter);
        }

        public async Task Join(IChatRoom room)
        {
            await Task.Delay(1);
            throw new NotImplementedException("Can't join chat rooms");
        }

        public async Task Exit(IChatRoom room)
        {
            await Task.Delay(1);
            throw new NotImplementedException("Can't exit chat rooms");

        }

        public bool ReadCommand(){
            Console.WriteLine("Waiting for a command...");
            currentCommand = Console.ReadLine();

            return !string.IsNullOrEmpty(currentCommand);

        } 

        public async Task<bool> ExecuteCommand(){
            //
            string[] commands = currentCommand.Split(' ');
            if (commands.Length == 0)
                return false;

            switch (commands[0])
            {
                case "joinonline":
                    CurrentGame = OnlineGame.Instance;

                    break;
                case "exitonline":

                    break;
                case "joinroom":
                    if (commands.Length == 1)
                        return false;

                    await this.Join(CurrentGame.GetRoom( commands[1]));
                    break;
                case "exitroom":
                    await this.Exit(CurrentGame.GetRoom( commands[1]));
                    break;
                case "quit":
                    await this.Disconnect();
                    break;
                default:
                    break;

            }

            return true;

        }

        public async Task DisplayMessage(string message, ICharacter character)
        {
            await Task.Delay(1);
            Console.WriteLine("Message to " + character.NickName + ": \""+message+" \"");

        }

        public async Task DisplayMessage(string message)
        {
            await Task.Delay(1);
            Console.WriteLine( message);

        }

    }

}
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medievalica.Game;
using Medievalica.Game.Interfaces;
using Medievalica.Game.Controllers;
using System.Net.WebSockets;

namespace Medievalica.Client {

    public class ConsoleClient : IGameClient {

        MainGame game;
        ICharacter  mainCharacter;

        string currentCommand = string.Empty;

        public ConsoleClient(){
            game = MainGame.Instance;
            
            mainCharacter = 
             CharacterController
             .LoadCharacterProfile(this)
             .GetAwaiter().GetResult();

            
        }

        public  async Task Connect(){
            await game.Connect(this);
            
        }

        public  async Task Disconnect(){

            await game.Disconnect(this);

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

        ClientWebSocket client;

        private async Task RunWebSockets()
        {
            client = new ClientWebSocket();
            await client.ConnectAsync(new Uri("ws://localhost:5000/test"), CancellationToken.None);

            Console.WriteLine("Connected!");

            bool endUpNow = false;
            string command = string.Empty;
            while (!endUpNow)
            {
                command = System.Console.ReadLine();

                if(command != "quit"){
                    endUpNow = true;
                    continue;
                } else
                {
                    await SendMessage(command);
                    System.Threading.Thread.Sleep(500);
                }

            }
            
            await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

            Console.WriteLine("Hello World!");
            Console.WriteLine("Premi un tasto per proseguire...");
            Console.ReadKey();
        }

        private async Task SendMessage(string message){

            var bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                
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
                case "joinroom":
                    if (commands.Length == 1)
                        return false;

                    await this.Join(GameRoomController.GetRoom(commands[1]));
                    break;
                case "exitroom":
                    await this.Exit(GameRoomController.GetRoom(commands[1]));
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
    }

}
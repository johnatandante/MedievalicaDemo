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

        public ConsoleClient(){
            game = MainGame.Instance;
            
            mainCharacter = 
             CharacterController
             .LoadCharacterProfile()
             .GetAwaiter().GetResult();

        }

        public  async Task Connect(){
            await game.Connect(this);
            
        }

        public  async Task Disconnect(){

            await game.Disconnect(this);

        }

        public async Task Join(IGameRoom room){
            throw new NotImplementedException("Can't join Game rooms");
       
        }

        public async Task Exit(IGameRoom room){
            throw new NotImplementedException("Can't exit game rooms");
       
        }

        public Task Join(IChatRoom room) {
            throw new NotImplementedException("Can't join chat rooms");
        }

        public Task Exit(IChatRoom room){
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
            Task.Delay(5000);

            return true;

        } 

        public void ExecuteCommand(){
            //
        }
    }

}
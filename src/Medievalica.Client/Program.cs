using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Program
    {
        static ClientWebSocket client;

        /// <summary>
        /// https://radu-matei.github.io/blog/aspnet-core-websockets-middleware/
        /// </summary>
        public static void Main(string[] args)
        {
            try{
            RunWebSockets().GetAwaiter().GetResult();
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        private static async Task RunWebSockets()
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

        private static async Task SendMessage(string message){

            var bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                
        }

    }
}

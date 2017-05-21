using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Client
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
                // RunWebSockets().GetAwaiter().GetResult();
                var client = new ConsoleClient();
                
                while (client.ReadCommand())
                {

                    client.ExecuteCommand();

                }

            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

    }
}

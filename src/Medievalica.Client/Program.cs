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
                ConsoleClient client = new ConsoleClient();
                
                while (client.ReadCommand())
                {

                    try
                    {
                        if (!client.ExecuteCommand().GetAwaiter().GetResult())
                            break;
                    }
                    catch (Exception ge)
                    {
                        Console.WriteLine(ge.Message);
                    }
                    finally
                    {
                        Task.Delay(1500).Wait();
                    }

                }

            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
            finally
            {

                Console.WriteLine("Exiting... press a key to continue...");
                Console.ReadKey();
            }
        }

    }
}

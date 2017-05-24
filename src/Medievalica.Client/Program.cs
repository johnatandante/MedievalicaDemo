using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Client
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try{

                ConsoleClient client = new ConsoleClient();
                client.SetName("Dante");

                client.Connect().Wait();
                
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

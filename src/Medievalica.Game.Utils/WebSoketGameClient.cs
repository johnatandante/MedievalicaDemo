using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Medievalica.Game.Utils.Commands;
using Medievalica.Game.Utils.Events;
using static Medievalica.Game.Utils.Events.CommandGameHelper;
using Medievalica.Game.Utils.Messages;

namespace Medievalica.Game.Utils
{
    /// <summary>
    /// https://radu-matei.github.io/blog/aspnet-core-websockets-middleware/
    /// </summary>
    public class WebSoketGameClient : IDisposable
    {

        static string uri = "ws://localhost:5000/ws";

        protected ClientWebSocket client;

        public bool Initialized
        {
            get
            {
                return client != null &&
                    (client.State == WebSocketState.Connecting
                      || client.State == WebSocketState.Open);
            }
        }

        public bool Connected
        {
            get
            {
                return client != null &&
                    client.State == WebSocketState.Open;
            }
        }

        private string TokenId { get; set; }

        public WebSoketGameClient()
        {
            client = new ClientWebSocket();
            
        }

        public event DataReadDelegate OnMessageReady;

        protected void Notify(object message)
        {
            if (OnMessageReady != null)
                OnMessageReady(this, new DataReadyEventArgs(message));
        }

        public async Task Connect()
        {
            if (Initialized)
                return;
            
            await client.ConnectAsync(new Uri(uri), CancellationToken.None);

        }

        public static async void TestWebSockets()
        {
            using (WebSoketGameClient client = new WebSoketGameClient())
            {
                Console.WriteLine("Connected!");

                bool endUpNow = false;
                string command = string.Empty;
                while (!endUpNow)
                {
                    command = System.Console.ReadLine();

                    if (command != "quit")
                    {
                        endUpNow = true;
                        continue;
                    }
                    else
                    {
                        await client.Send(command);
                        System.Threading.Thread.Sleep(500);
                    }

                }

                Console.WriteLine("Hello World!");
                Console.WriteLine("Premi un tasto per proseguire...");
                Console.ReadKey();
            }
        }

        public async Task Send(ICommand command)
        {
            await Send(JsonConvert.SerializeObject(command));

        }

        async Task Send(string message)
        {
            if (!Connected)
                return;
            try
            {
                await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on sending:" + e.Message);
            }
        }

        async Task SendStream(byte[] message)
        {

            if (!Connected)
                return;

            try
            {
                await client.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<T> Receive<T>()
        {
            T result;
            try
            {
                result = JsonConvert.DeserializeObject<T>(await ReceiveJson());
            }
            catch (Exception e)
            {
                result = default(T);

                Console.WriteLine("Error on receving:" + e.Message);
            }

            return result;
        }

        async Task<string> ReceiveJson()
        {
            try
            {

                byte[] buffer = new byte[1024 * 4];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                return Encoding.UTF8.GetString(buffer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return string.Empty;
            }

        }

        async Task<byte[]> ReceiveStream()
        {
            try
            {

                byte[] buffer = new byte[1024 * 4];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                return buffer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return new byte[] { };
            }
        }

        public async Task ReConnect()
        {
            if (Connected)
                await Disconnect();

            await Connect();
        }

        public async Task Disconnect()
        {
            await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

        }

        public async void Dispose()
        {
            if (Connected)
                await Disconnect();

        }
    }

}

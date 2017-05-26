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

namespace Medievalica.Game.Utils
{
    /// <summary>
    /// https://radu-matei.github.io/blog/aspnet-core-websockets-middleware/
    /// </summary>
    public class WebSoketGameClient : IDisposable
    {

        Task loop;

        ClientWebSocket client;

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

        public WebSoketGameClient()
        {
            client = new ClientWebSocket();

            //client.Options.KeepAliveInterval

        }

        public event DataReadDelegate OnDataReady;

        public async Task Connect()
        {
            if (Initialized)
                return;

            await client.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);

            loop = Task.Run(
                () =>
                {
                    while (Connected)
                    {
                        string result = ReceiveJson().GetAwaiter().GetResult();
                        if (OnDataReady != null)
                            OnDataReady(this, new DataReadyEventArgs(JsonConvert.DeserializeObject<IResultCommand>(result)));

                        Task.Delay(750).Wait();

                    }

                }
            );
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

        public async Task Send(string message)
        {
            if (!Connected)
                return;
            try
            {
                await client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task SendStream(byte[] message)
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
            catch (Exception)
            {
                result = default(T);
            }

            return result;
        }

        public async Task<string> ReceiveJson()
        {
            try
            {

                var segment = new ArraySegment<byte>();
                var result = await client.ReceiveAsync(segment, CancellationToken.None);

                return Encoding.UTF8.GetString(segment.Array);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return string.Empty;
            }

        }

        public async Task<byte[]> ReceiveStream()
        {
            try
            {

                var segment = new ArraySegment<byte>();
                var result = await client.ReceiveAsync(segment, CancellationToken.None);

                return segment.Array;
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

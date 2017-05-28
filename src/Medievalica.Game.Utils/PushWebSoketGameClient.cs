using Medievalica.Game.Utils.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Medievalica.Game.Utils
{
    public class PushWebSoketGameClient : WebSoketGameClient
    {
        
        static string pushuri = "ws://localhost:5000/pushws";
        
        public async Task ListenToStream()
        {

            if (Initialized)
                return;

            await client.ConnectAsync(new Uri(pushuri), CancellationToken.None);

            try
            {
                byte[] buffer = new byte[1024 * 4];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer)
                    , CancellationToken.None);

                while (!result.CloseStatus.HasValue)
                {
                    object message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(buffer));

                    Notify(message);

                    result = await client.ReceiveAsync(new ArraySegment<byte>(buffer)
                        , CancellationToken.None);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }


        }

    }
}

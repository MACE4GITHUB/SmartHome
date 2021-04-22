using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Client
{
    public static class Program
    {
        private static int _counter;

        public static async Task Main()
        {
            var sensorId = new Guid("2223cc1a-0be0-4ea4-a7b1-e905d23e8e9c");
            using var client = new HttpClient();

            async Task SendData(int value)
            {
                await Task.Yield();

                var data = new
                {
                    SensorId = sensorId,
                    Timestamp = DateTime.UtcNow,
                    Value = GetNextValue()
                };

                using var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                // TODO: Hardcode the actual URL here.
                await client.PostAsync("http://localhost:5000/api/v1/data/save", content)
                    .ConfigureAwait(false);
            }

            while (true)
            {
                await Task.WhenAll(Enumerable.Range(1, 100_000)
                        .Select(_ => SendData(GetNextValue()))
                        .ToList())
                    .ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromSeconds(1))
                    .ConfigureAwait(false);

                Console.Write(".");
            }
        }

        private static int GetNextValue()
        {
            if (_counter > 200)
            {
                Interlocked.Exchange(ref _counter, -5);
            }

            return Interlocked.Increment(ref _counter);
        }
    }
}

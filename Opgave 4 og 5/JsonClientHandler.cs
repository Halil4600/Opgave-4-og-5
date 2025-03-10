using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace Opgave_4_og_5
{
    public class JsonClientHandler
    {
        private readonly TcpClient _client;
        public JsonClientHandler(TcpClient client) => _client = client;

        public async Task HandleJsonClientAsync()
        {
            using NetworkStream ns = _client.GetStream();
            using StreamReader reader = new StreamReader(ns);
            using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            bool isRunning = true;

            while (isRunning)
            {
                string jsonRequest = await reader.ReadLineAsync();
                if (jsonRequest == null) return;

                var request = JsonSerializer.Deserialize<Request>(jsonRequest);
                Console.WriteLine($"Received JSON: {jsonRequest}");
                string result = null;

                try
                {
                    result = request.Method switch
                    {
                        "Random" => new Random().Next(request.Numbers[0], request.Numbers[1] + 1).ToString(),
                        "Add" => request.Numbers.Sum().ToString(),
                        "Subtract" => request.Numbers.Aggregate((a, b) => a - b).ToString(),
                        "close" => "connection closed",
                        _ => throw new InvalidOperationException("Invalid command")
                    };
                }
                catch (Exception ex)
                {
                    result = $"Error: {ex.Message}";
                }

                var response = new Response
                {
                    Method = request.Method,
                    Numbers = request.Numbers,
                    Result = result
                };

                await writer.WriteLineAsync(JsonSerializer.Serialize(response));
                Console.WriteLine($"Sent JSON: {JsonSerializer.Serialize(response)}");
                _client.Close();
            }
        }
      
        private class Request
        {
            public string Method { get; set; }
            public int[] Numbers { get; set; }
        }

        private class Response
        {
            public string Method { get; set; }
            public int[] Numbers { get; set; }
            public string Result { get; set; }
        }
    }
}

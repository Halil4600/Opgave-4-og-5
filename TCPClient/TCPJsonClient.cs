using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCPClient
{
    public class TCPJsonClient
    {
        public async Task StartClient()
        {
            using TcpClient client = new TcpClient("127.0.0.1", 8);
            using NetworkStream ns = client.GetStream();
            using StreamReader reader = new StreamReader(ns);
            using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("Enter method (Random/Add/Subtract/close): ");
                string method = Console.ReadLine();

                if (method == "close")
                {
                    isRunning = false;
                    continue;
                }

                Console.Write("Enter numbers (separated by space): ");
                int[] numbers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                var request = new { Method = method, Numbers = numbers };
                string jsonRequest = JsonSerializer.Serialize(request);
                writer.WriteLineAsync(jsonRequest);

                string jsonResponse = reader.ReadLine();
                Console.WriteLine($"Server Response: {jsonResponse}");
            }
        }
    }
}

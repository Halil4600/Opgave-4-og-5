using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    public class TCPStringClient
    {
        public async Task StartClient()
        {
            using TcpClient client = new TcpClient("127.0.0.1", 7);
            using NetworkStream ns = client.GetStream();
            using StreamReader reader = new StreamReader(ns);
            using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            Console.Write("Enter command (Random/Add/Subtract/close): ");
            string command = Console.ReadLine();
            await writer.WriteLineAsync(command);

            if (command != "close")
            {
                Console.Write("Enter numbers (separated by space): ");
                string numbers = Console.ReadLine();
                await writer.WriteLineAsync(numbers);
            }

            string response = await reader.ReadLineAsync();
            Console.WriteLine($"Server Response: {response}");
        }
    }
}

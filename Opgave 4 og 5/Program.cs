
using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GSF.Communication;
using Opgave_4_og_5;


namespace Opgave_4_og_5
{
    public class Program
    {
        public static async Task Main()
        {
            Task.Run(() => new TCPServer().TcpServerStart());
            Task.Run(() => new JsonTCPServer().JsonTcpServerStart());

            string choice = Console.ReadLine();
        }
    }
}

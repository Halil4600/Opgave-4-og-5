using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_4_og_5
{
    public class ClientHandler
    {
        public static void HandleClient(TcpClient socket)
        {
            Console.WriteLine(socket.Client.RemoteEndPoint);
            using NetworkStream ns = socket.GetStream();
            using StreamReader reader = new StreamReader(ns);
            using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            bool isRunning = true;
            while (isRunning)
            {
                string message = reader.ReadLine();

                switch (message)
                {
                    case "Random":
                        writer.WriteLine(RNum(reader.ReadLine()));
                        break;
                    case "Add":
                        writer.WriteLine(ANum(reader.ReadLine()));
                        break;
                    case "Subtract":
                        writer.WriteLine(SNum(reader.ReadLine()));
                        break;
                    case "close":
                        writer.WriteLine("connection closed");
                        isRunning = false;
                        break;
                }
            }
        }

        public static int RNum(string message)
        {
            string[] numbers = message.Split(' ');
            if (numbers.Length != 2 || !int.TryParse(numbers[0], out int min) || !int.TryParse(numbers[1], out int max)) return -1;
            if (min > max) (min, max) = (max, min);
            return new Random().Next(min, max + 1);
        }

        public static int ANum(string message)
        {
            string[] numbers = message.Split(' ');
            return numbers.Sum(int.Parse);
        }

        public static int SNum(string message)
        {
            string[] numbers = message.Split(' ');
            return numbers.Select(int.Parse).Aggregate((a, b) => a - b);
        }
    }
}


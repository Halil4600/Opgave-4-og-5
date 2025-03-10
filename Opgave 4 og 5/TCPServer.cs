using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_4_og_5
{
    public class TCPServer
    {
        public void TcpServerStart()
        {
            Console.WriteLine("TCP Server started...");
            int port = 7;
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => ClientHandler.HandleClient(client));
            }
        }
    }
}

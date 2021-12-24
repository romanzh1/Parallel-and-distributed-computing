using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ConnectClient
    {
        TcpListener server;
        Socket client;
        static List<ConnectClient> allСonnections = new List<ConnectClient>();
        bool b = true;
        public ConnectClient(TcpListener newListener)
        {
            server = newListener;
            client = server.AcceptSocket();
            allСonnections.Add(this);
        }

        public void StartRead()
        {
            try
            {
                while (true)
                {
                    byte[] readData = new byte[1024];
                    int n = client.Receive(readData);
                    if (n != 0)
                    {

                        string m = ReadMessage(readData, n);
                        WriteMessage(m, n);
                    }
                    else
                        break;
                }
            }
            catch
            {
                allСonnections.Remove(this);
                client.Shutdown(SocketShutdown.Both);
            }
        }

        private string ReadMessage(byte[] RD, int n)
        {
            byte[] readData = RD;
            int bytes_count = n;
            string message = Encoding.GetEncoding(1251).GetString(readData.Where(x => x != 0).ToArray());
            if (message.Contains("-GETCOLOR"))
            {
                if (b)
                    return "-" + allСonnections.Count;
                b = false;
            }

            return message;

        }

        private void WriteMessage(string message, int n)
        {
            if (n != 0)
                foreach (ConnectClient item in allСonnections)
                {
                    item.client.Send(Encoding.GetEncoding(1251).GetBytes(message));
                }
        }
    }
}

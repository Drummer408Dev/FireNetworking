using FireNetworking.Common.Messages;
using System;
using System.Net.Sockets;
using FireNetworking.Common.Network;

namespace FireNetworking.Client
{
    public class Client
    {
        private string host;
        private int port;
        private TcpClient tcpClient;
        private IStream stream;

        public Client(string host, int port)
        {
            this.host = host;
            this.port = port;
            tcpClient = new TcpClient();
        }

        public bool Connect()
        {
            try
            {
                tcpClient.Connect(host, port);
                stream = new BinaryStream(tcpClient.GetStream());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public void Write(IMessageRequest request)
        {
            stream.Write(request);
        }

        public IMessageResponse Read()
        {
            return (IMessageResponse) stream.Read();
        }

        public void Disconnect()
        {
            tcpClient.Dispose();
        }
    }
}

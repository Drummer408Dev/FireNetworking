using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using FireNetworking.Common.MessageHandlers;
using FireNetworking.Common.Messages;
using FireNetworking.Common.Network;

namespace FireNetworking.Server
{
    public class Server
    {
        private TcpListener listener;
        private IStream stream;

        public Server(string host, int port)
        {
            listener = new TcpListener(IPAddress.Parse(host), 8888);
        }

        public void Start()
        {
            listener.Start();
            Console.WriteLine("Server has started....");
            while (true)
            {
                var client = listener.AcceptTcpClient();
                var clientId = Guid.NewGuid();
                var clientHandler = new NetworkClientHandler(clientId, client);
                Task.Factory.StartNew(() => clientHandler.Handle());
            }
        }

        public void RegisterMessageHandler<T>(MessageHandler<T> messageHandler) where T : IMessageRequest
        {
            NetworkClientHandler.MessageHandlerRepository.AddMessageHandler(messageHandler);
        }
    }
}

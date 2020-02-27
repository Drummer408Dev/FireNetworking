using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using FireNetworking.Common.MessageHandlers;
using FireNetworking.Common.Messages;
using FireNetworking.Common.Network;

namespace FireNetworking.Server
{
    internal class NetworkClientHandler
    {
        internal static MessageHandlerRepository MessageHandlerRepository = new MessageHandlerRepository();

        private static ConcurrentDictionary<Guid, TcpClient> connectedClients = new ConcurrentDictionary<Guid, TcpClient>();

        private Guid clientId;

        public NetworkClientHandler(Guid clientId, TcpClient client)
        {
            this.clientId = clientId;
            connectedClients.TryAdd(clientId, client);
        }

        public void Handle()
        {
            Console.WriteLine($"Client connected: {clientId.ToString()}");

            var client = connectedClients[clientId];
            using (var stream = new BinaryStream(client.GetStream()))
            {
                while (connectedClients.ContainsKey(clientId) && client.Connected)
                {
                    try
                    {
                        HandleIncomingMessage(stream);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine($"Client disconnected: {clientId.ToString()}");
                        connectedClients.TryRemove(clientId, out var val);
                    }
                }
            }
        }

        private void HandleIncomingMessage(IStream stream)
        {
            var request = (IMessageRequest)stream.Read();
            var messageHandler = MessageHandlerRepository.GetMessageHandler(request.GetType());
            var response = messageHandler.Handle(request);
            if (response != null)
                stream.Write(response);
        }
    }
}
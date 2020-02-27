using System.IO;
using System.Net.Sockets;
using FireNetworking.Common.Messages;

namespace FireNetworking.Common.Network
{
    public class BinaryStream : IStream
    {
        private BinaryWriter writer;
        private BinaryReader reader;
        private MessageTranslator messageTranslator;

        public BinaryStream(NetworkStream networkStream)
        {
            writer = new BinaryWriter(networkStream);
            reader = new BinaryReader(networkStream);
            messageTranslator = new MessageTranslator();
        }

        public void Write(IMessage message)
        {
            var serializedMessage = messageTranslator.Serialize(message);
            writer.Write(serializedMessage);
        }

        public IMessage Read()
        {
            var serializedMessage = reader.ReadString();
            var message = messageTranslator.Deserialize(serializedMessage);
            return message;
        }

        public void Dispose()
        {
            writer.Close();
            reader.Close();
        }
    }
}
using FireNetworking.Common.Messages;
using Newtonsoft.Json;

namespace FireNetworking.Common.Network
{
    internal class MessageTranslator
    {
        public string Serialize(IMessage message)
        {
            var networkMessage = new NetworkMessage
            {
                Type = message.GetType(),
                Contents = JsonConvert.SerializeObject(message)
            };

            var serializedNetworkMessage = JsonConvert.SerializeObject(networkMessage);
            return serializedNetworkMessage;
        }

        public IMessage Deserialize(string serializedNetworkMessage)
        {
            var networkMessage = JsonConvert.DeserializeObject<NetworkMessage>(serializedNetworkMessage);
            var message = (IMessage)JsonConvert.DeserializeObject(networkMessage.Contents, networkMessage.Type);
            return message;
        }
    }
}
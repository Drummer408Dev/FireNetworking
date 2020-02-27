using System;
using System.Collections.Generic;
using FireNetworking.Common.Messages;

namespace FireNetworking.Common.MessageHandlers
{
    public class MessageHandlerRepository
    {
        private Dictionary<Type, IMessageHandler> messageHandlers;

        public MessageHandlerRepository()
        {
            messageHandlers = new Dictionary<Type, IMessageHandler>();
        }

        public void AddMessageHandler<T>(MessageHandler<T> messageHandler) where T : IMessageRequest
        {
            var type = typeof(T);
            if (messageHandlers.ContainsKey(type))
                throw new Exception($"Type {type} already registered.");
            
            messageHandlers.Add(type, messageHandler);
        }

        public IMessageHandler GetMessageHandler(Type type)
        {
            return messageHandlers[type];
        }

        public bool HandlerExists(Type type)
        {
            return messageHandlers.ContainsKey(type);
        }
    }
}

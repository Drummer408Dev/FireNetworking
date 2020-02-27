using FireNetworking.Common.Messages;

namespace FireNetworking.Common.MessageHandlers
{
    public abstract class MessageHandler<T> : IMessageHandler where T : IMessageRequest
    {
        IMessageResponse IMessageHandler.Handle(IMessageRequest message)
        {
            return Handle((T) message);
        }

        public abstract IMessageResponse Handle(T request);
    }
}
using FireNetworking.Common.Messages;

namespace FireNetworking.Common.MessageHandlers
{
    public interface IMessageHandler
    {
        IMessageResponse Handle(IMessageRequest request);
    }
}
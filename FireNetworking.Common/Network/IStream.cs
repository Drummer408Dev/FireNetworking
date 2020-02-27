using System;
using FireNetworking.Common.Messages;

namespace FireNetworking.Common.Network
{
    public interface IStream : IDisposable
    {
        void Write(IMessage message);
        IMessage Read();
    }
}
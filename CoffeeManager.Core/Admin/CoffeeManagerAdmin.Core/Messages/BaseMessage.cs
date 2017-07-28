using System;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core
{
    public class BaseMessage : MvxMessage
    {
        public BaseMessage(object sender) : base(sender)
        {
        }
    }

    public class BaseMessage<T> : BaseMessage
    {
        public BaseMessage(T data, object sender) : base(sender)
        {
            Data = data;
        }

        public T Data { get; }
    }
}

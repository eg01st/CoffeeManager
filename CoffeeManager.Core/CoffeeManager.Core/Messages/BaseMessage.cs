using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.Messages
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

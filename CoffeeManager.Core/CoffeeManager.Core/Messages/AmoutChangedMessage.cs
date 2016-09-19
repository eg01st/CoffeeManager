using System;

namespace CoffeeManager.Core.Messages
{
    public class AmoutChangedMessage : BaseMessage<Tuple<float, bool>>
    {
        public AmoutChangedMessage(Tuple<float, bool> data, object sender) : base(data, sender)
        {
        }
    }
}

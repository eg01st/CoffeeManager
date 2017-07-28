using System;

namespace CoffeeManager.Core.Messages
{
    public class AmoutChangedMessage : BaseMessage<Tuple<Decimal, bool>>
    {
        public AmoutChangedMessage(Tuple<decimal, bool> data, object sender) : base(data, sender)
        {
        }
    }
}

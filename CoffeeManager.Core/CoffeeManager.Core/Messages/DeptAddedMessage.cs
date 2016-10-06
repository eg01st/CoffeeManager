using System;

namespace CoffeeManager.Core.Messages
{
    public class DeptAddedMessage : BaseMessage<Tuple<Decimal, bool>>
    {
        public DeptAddedMessage(Tuple<decimal, bool> data, object sender) : base(data, sender)
        {
        }
    }
}

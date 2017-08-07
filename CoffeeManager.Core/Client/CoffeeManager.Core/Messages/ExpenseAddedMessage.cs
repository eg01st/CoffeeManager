using System;

namespace CoffeeManager.Core.Messages
{
    public class ExpenseAddedMessage : BaseMessage<Decimal>
    {
        public ExpenseAddedMessage(decimal data, object sender) : base(data, sender)
        {
        }
    }
}

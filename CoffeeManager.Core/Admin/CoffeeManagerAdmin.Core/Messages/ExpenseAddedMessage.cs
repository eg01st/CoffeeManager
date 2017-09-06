using System;
namespace CoffeeManagerAdmin.Core
{
    public class ExpenseAddedMessage : BaseMessage
    {
        public ExpenseAddedMessage(object sender) : base(sender)
        {
        }
    }
}

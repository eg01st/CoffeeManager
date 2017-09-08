using System;
namespace CoffeeManagerAdmin.Core
{
    public class ExpenseListChangedMessage : BaseMessage
    {
        public ExpenseListChangedMessage(object sender) : base(sender)
        {
        }
    }
}

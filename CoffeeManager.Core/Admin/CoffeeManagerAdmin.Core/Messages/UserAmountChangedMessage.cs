using System;
namespace CoffeeManagerAdmin.Core
{
    public class UserAmountChangedMessage : BaseMessage
    {
        public UserAmountChangedMessage(object sender) : base(sender)
        {
        }
    }
}

using System;
namespace CoffeeManagerAdmin.Core
{
    public class SuplyListChangedMessage : BaseMessage
    {
        public SuplyListChangedMessage(object sender) : base(sender)
        {
        }
    }
}

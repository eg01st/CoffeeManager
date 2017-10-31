using System;
namespace CoffeeManagerAdmin.Core
{
    public class CoffeeRoomChangedMessage : BaseMessage
    {
        public CoffeeRoomChangedMessage(object sender) : base(sender)
        {
        }
    }
}

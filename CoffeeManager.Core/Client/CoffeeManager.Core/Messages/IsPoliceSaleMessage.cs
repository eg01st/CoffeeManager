namespace CoffeeManager.Core.Messages
{
    public class IsPoliceSaleMessage : BaseMessage<bool>
    {
        public IsPoliceSaleMessage(bool data, object sender) : base(data, sender)
        {
        }
    }
}

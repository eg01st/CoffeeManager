namespace CoffeeManager.Core.Messages
{
    public class IsCreditCardSaleMessage : BaseMessage<bool>
    {
        public IsCreditCardSaleMessage(bool data, object sender) : base(data, sender)
        {
        }
    }
}

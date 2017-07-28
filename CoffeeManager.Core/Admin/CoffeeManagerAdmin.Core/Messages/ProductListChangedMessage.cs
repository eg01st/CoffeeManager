using System;
namespace CoffeeManagerAdmin.Core
{
    public class ProductListChangedMessage : BaseMessage
    {
        public ProductListChangedMessage(object obj) : base(obj)
        {
        }
    }
}

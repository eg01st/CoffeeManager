using System;
using MvvmCross.Plugins.Messenger;
namespace CoffeeManagerAdmin.Core
{
    public class ProductSelectedMessage : BaseMessage<ProductItemViewModel>
    {
        public ProductSelectedMessage(ProductItemViewModel data, object sender) : base(data, sender)
        {
        }
    }
}

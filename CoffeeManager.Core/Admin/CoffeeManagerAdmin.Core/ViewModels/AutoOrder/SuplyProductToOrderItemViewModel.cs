using CoffeeManager.Models;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class SuplyProductToOrderItemViewModel : FeedItemElementViewModel
    {
        private int quantityShouldBeAfterOrder;

        public SuplyProductToOrderItemViewModel(int suplyProductId, string suplyProductName)
        {
            SuplyProductId = suplyProductId;
            SuplyProductName = suplyProductName;
        }
        
        public int Id { get; set; }

        public int SuplyProductId { get; set; }

        public string SuplyProductName { get; set; }

        public int QuantityShouldBeAfterOrder
        {
            get => quantityShouldBeAfterOrder;
            set => SetProperty(ref quantityShouldBeAfterOrder, value);
        }
    }
}
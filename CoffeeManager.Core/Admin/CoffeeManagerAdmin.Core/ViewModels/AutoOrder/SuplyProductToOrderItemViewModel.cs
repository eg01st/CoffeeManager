using CoffeeManager.Models;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class SuplyProductToOrderItemViewModel : FeedItemElementViewModel
    {
        private int quantityShouldBeAfterOrder;

        public SuplyProductToOrderItemViewModel(int suplyProductId, string suplyProductName, bool isEditable = true)
        {
            SuplyProductId = suplyProductId;
            SuplyProductName = suplyProductName;
            IsEditable = isEditable;
        }
        
        public int Id { get; set; }

        public int SuplyProductId { get; set; }

        public string SuplyProductName { get; set; }

        public bool IsEditable { get; set; }

        public int QuantityShouldBeAfterOrder
        {
            get => quantityShouldBeAfterOrder;
            set => SetProperty(ref quantityShouldBeAfterOrder, value);
        }
    }
}
using CoffeeManager.Models;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class SuplyProductToOrderItemViewModel : FeedItemElementViewModel
    {
        private int quantityShouldBeAfterOrder;
        private bool shouldUpdateQuantityBeforeOrder;

        public SuplyProductToOrderItemViewModel(int suplyProductId, string suplyProductName, bool isEditable = true)
        {
            SuplyProductId = suplyProductId;
            SuplyProductName = suplyProductName;
            IsEditable = isEditable;
            
            ToggleShouldUpdateQuantityBeforeOrderCommand = new MvxCommand(() => ShouldUpdateQuantityBeforeOrder = !ShouldUpdateQuantityBeforeOrder);
        }
        
        public IMvxCommand ToggleShouldUpdateQuantityBeforeOrderCommand { get; }
        
        public int Id { get; set; }

        public int SuplyProductId { get; set; }

        public string SuplyProductName { get; set; }

        public bool IsEditable { get; set; }

        public bool ShouldUpdateQuantityBeforeOrder
        {
            get => shouldUpdateQuantityBeforeOrder;
            set => SetProperty(ref shouldUpdateQuantityBeforeOrder, value);
        }

        public int QuantityShouldBeAfterOrder
        {
            get => quantityShouldBeAfterOrder;
            set => SetProperty(ref quantityShouldBeAfterOrder, value);
        }
    }
}
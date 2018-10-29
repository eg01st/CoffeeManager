using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class SelectSuplyProductItemViewModel : ListItemViewModelBase
    {
        private bool isSelected;
        
        public SelectSuplyProductItemViewModel(SupliedProduct supliedProduct)
        {
            SupliedProduct = supliedProduct;
            Name = supliedProduct.Name;
        }
        
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }
                
        public SupliedProduct SupliedProduct { get; private set; }
    }
}
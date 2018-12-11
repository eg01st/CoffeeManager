using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class SelectSuplyProductItemViewModel : ListItemViewModelBase
    {
        private bool isSelected;
        
        public SelectSuplyProductItemViewModel(SupliedProduct supliedProduct)
        {
            SupliedProduct = supliedProduct;
            Name = supliedProduct.Name;
            ToggleSelectedCommand = new MvxCommand(DoToggleSelected);
        }

        private void DoToggleSelected()
        {
            IsSelected = !IsSelected;
        }

        public ICommand ToggleSelectedCommand { get; }

        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }
                
        public SupliedProduct SupliedProduct { get; private set; }
    }
}
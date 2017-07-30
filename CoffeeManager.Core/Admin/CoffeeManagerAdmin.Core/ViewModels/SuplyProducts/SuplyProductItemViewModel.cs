using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SuplyProductItemViewModel : ListItemViewModelBase
    {
        private SupliedProduct _item;
        public SuplyProductItemViewModel(SupliedProduct product)
        {
            _item = product;
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<SuplyProductDetailsViewModel>(new { id = _item.Id });
        }

        public override string Name => _item.Name;

        public decimal Price => _item.Price;

        public string Quatity => _item.Quatity?.ToString("F");
    }
}

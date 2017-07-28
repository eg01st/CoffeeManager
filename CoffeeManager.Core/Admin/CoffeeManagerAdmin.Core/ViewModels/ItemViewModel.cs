using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ItemViewModel : ListItemViewModelBase
    {
        private Product _prod;
        public ItemViewModel(Product prod)
        {
            _prod = prod;
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<CalculationViewModel>(new { id = _prod.Id });
        }

        public int Id => _prod.Id;

        public string Name => _prod.Name + " : " + _prod.Price.ToString("F1");


    }
}

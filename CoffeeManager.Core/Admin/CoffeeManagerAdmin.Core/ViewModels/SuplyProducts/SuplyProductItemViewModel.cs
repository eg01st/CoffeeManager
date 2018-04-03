using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.SuplyProducts
{
    public class SuplyProductItemViewModel : ListItemViewModelBase
    {
        private readonly ISuplyProductsManager manager;
        private SupliedProduct _item;

        public SuplyProductItemViewModel(SupliedProduct product)
        {
            _item = product;
            manager = Mvx.Resolve<ISuplyProductsManager>();
            DeleteItemCommand = new MvxCommand(DoDeleteItem);
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<SuplyProductDetailsViewModel>(new { id = _item.Id });
        }

        public ICommand DeleteItemCommand { get; set; }

        public override string Name => _item.Name;

        public decimal Price => _item.Price;

        public int Id => _item.Id;

        public string Quatity => _item.Quatity?.ToString("F");

        public string ExpenseTypeName => _item.ExpenseTypeName;

        private void DoDeleteItem()
        {
            Confirm($"Действительно удалить товар {_item.Name}?", OnDeleteItem);
        }

        private async Task OnDeleteItem()
        {
            await manager.DeleteSuplyProduct(_item.Id);
            Publish(new SuplyListChangedMessage(this));
        }
    }
}

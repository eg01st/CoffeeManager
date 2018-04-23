using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Calculation
{
    public class CalculationViewModel : ViewModelBase, IMvxViewModel<int>
    {
        private readonly MvxSubscriptionToken listChangedToken;
        private string name;
        private List<CalculationItemViewModel> items;
        private ICommand _addItemCommand;
        private int productId;
        readonly ISuplyProductsManager manager;

        public CalculationViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            _addItemCommand = new MvxAsyncCommand(DoAddItem);
            listChangedToken = Subscribe<CalculationListChangedMessage>(async (obj) => await Initialize());
        }

        private async Task DoAddItem()
        {
            await NavigationService.Navigate<SelectCalculationListViewModel, int>(productId);
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(LoadData);
        }

        private async Task LoadData()
        {
            var info = await manager.GetProductCalculationItems(productId);
            productId = info.ProductId;
            Name = info.Name;
            Items = info.SuplyProductInfo.Select(s => new CalculationItemViewModel(manager, s)).ToList();
        }

        public ICommand AddItemCommand => _addItemCommand;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public List<CalculationItemViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        protected override void DoUnsubscribe()
        {
            Unsubscribe<CalculationListChangedMessage>(listChangedToken);
        }

        public void Prepare(int parameter)
        {
            productId = parameter;
        }
    }
}

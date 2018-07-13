using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using CoffeManager.Common.Managers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Calculation
{
    public class SelectCalculationListViewModel : BaseAdminSearchViewModel<SelectCalculationItemViewModel>, IMvxViewModel<int>
    {
        private int productId;
        private MvxSubscriptionToken _listChanged;

        readonly ISuplyProductsManager manager;

        public ICommand AddNewSuplyProductCommand { get; }

        public SelectCalculationListViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            _listChanged = MvxMessenger.Subscribe<SuplyListChangedMessage>(async (obj) => await Initialize());
            AddNewSuplyProductCommand = new MvxAsyncCommand(async() => await NavigationService.Navigate<AddSuplyProductViewModel>());
        }

        public override async Task<List<SelectCalculationItemViewModel>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            return items.Select(s => new SelectCalculationItemViewModel(manager, productId, s)).ToList();
        }
        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<SuplyListChangedMessage>(_listChanged);
        }

        public void Prepare(int parameter)
        {
            productId = parameter;
        }
    }
}

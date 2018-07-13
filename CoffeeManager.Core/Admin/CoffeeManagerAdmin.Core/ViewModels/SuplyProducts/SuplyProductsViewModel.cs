using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.SuplyProducts
{
    public class SuplyProductsViewModel : BaseAdminSearchViewModel<ListItemViewModelBase>
    {
        private MvxSubscriptionToken _listChanged;

        private readonly ISuplyProductsManager manager;

        public ICommand AddNewSuplyProductCommand { get; }

        public override bool ShouldReloadOnCoffeeRoomChange => true;

        public SuplyProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            AddNewSuplyProductCommand = new MvxAsyncCommand(async() => await NavigationService.Navigate<AddSuplyProductViewModel>());
        }

        protected override void DoSubscribe()
        {
            base.DoSubscribe();

            _listChanged = MvxMessenger.Subscribe<SuplyListChangedMessage>(async (obj) => await Initialize());
        }

        public override async Task<List<ListItemViewModelBase>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            var result = new List<ListItemViewModelBase>();


            var vms = items.Select(s => new SuplyProductItemViewModel(s)).GroupBy(g => g.ExpenseTypeName).OrderByDescending(o => o.Key);
            foreach (var item in vms)
            {
                result.Add(new ExpenseTypeHeaderViewModel(item.Key));
                result.AddRange(item);
            }
            return result;
        }

        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<SuplyListChangedMessage>(_listChanged);
        }
    }
}

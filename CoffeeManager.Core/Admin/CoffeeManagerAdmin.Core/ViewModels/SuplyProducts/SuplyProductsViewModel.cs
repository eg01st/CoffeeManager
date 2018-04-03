using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.SuplyProducts
{
    public class SuplyProductsViewModel : BaseSearchViewModel<ListItemViewModelBase>
    {
        private MvxSubscriptionToken _listChanged;

        readonly ISuplyProductsManager manager;

        public ICommand AddNewSuplyProductCommand { get; }


        public SuplyProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            _listChanged = MvxMessenger.Subscribe<SuplyListChangedMessage>((obj) => base.Initialize());
            AddNewSuplyProductCommand = new MvxCommand(() => ShowViewModel<AddSuplyProductViewModel>());
        }

        public async override Task<List<ListItemViewModelBase>> LoadData()
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

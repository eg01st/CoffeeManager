using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.ManageExpenses
{
    public class MapExpenseToSuplyProductViewModel : BaseSearchViewModel<SelectMappedSuplyProductItemViewModel>, IMvxViewModel<int>
    {
        private MvxSubscriptionToken _listChanged;
        private int expenseTypeId;
        readonly ISuplyProductsManager suplyProductManager;

        public ICommand AddNewSuplyProductCommand { get; }

        public MapExpenseToSuplyProductViewModel(ISuplyProductsManager suplyProductManager)
        {
            this.suplyProductManager = suplyProductManager;
            _listChanged = MvxMessenger.Subscribe<SuplyListChangedMessage>(async (obj) => await Initialize());
            AddNewSuplyProductCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AddSuplyProductViewModel>());
        }

        public override async Task<List<SelectMappedSuplyProductItemViewModel>> LoadData()
        {
		    var items = await suplyProductManager.GetSuplyProducts();
            return items.Where(i => i.ExpenseTypeId != expenseTypeId).Select(s => new SelectMappedSuplyProductItemViewModel(s, expenseTypeId)).ToList();
        }

        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<SuplyListChangedMessage>(_listChanged);
        }

        public void Prepare(int parameter)
        {
            expenseTypeId = parameter;
        }
    }
}

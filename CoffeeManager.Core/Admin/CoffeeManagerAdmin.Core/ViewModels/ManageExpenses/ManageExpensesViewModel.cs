using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.ManageExpenses
{
    public class ManageExpensesViewModel : BaseSearchViewModel<ManageExpenseItemViewModel>
    {
        private readonly IPaymentManager manager;
        private readonly MvxSubscriptionToken reloadListToken;

        public ICommand AddExpenseTypeCommand { get; set; }

        public ManageExpensesViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            AddExpenseTypeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AddExpenseTypeViewModel>());
            reloadListToken = MvxMessenger.Subscribe<ExpenseListChangedMessage>(async (obj) => await Initialize());
        }

        public override async Task<List<ManageExpenseItemViewModel>> LoadData()
        {
            return await ExecuteSafe(async () =>
            {
                var items = await manager.GetExpenseItems();
                return items.Select(s => new ManageExpenseItemViewModel(manager, s)).ToList();
            });
        }

        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<ExpenseListChangedMessage>(reloadListToken);
        }

    }
}

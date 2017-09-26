using System.Collections.Generic;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core
{
    public class ManageExpensesViewModel : BaseSearchViewModel<ManageExpenseItemViewModel>
    {
        private readonly IPaymentManager manager;
        private readonly MvxSubscriptionToken reloadListToken;

        public ICommand AddExpenseTypeCommand { get; set; }

        public ManageExpensesViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            AddExpenseTypeCommand = new MvxCommand(() => ShowViewModel<AddExpenseTypeViewModel>());
            reloadListToken = Subscribe<ExpenseListChangedMessage>(async (obj) => await Init());
        }

        public async override Task<List<ManageExpenseItemViewModel>> LoadData()
        {
            return await ExecuteSafe(async () =>
            {
                var items = await manager.GetExpenseItems();
                return items.Select(s => new ManageExpenseItemViewModel(manager, s)).ToList();
            });
        }

        protected override void DoUnsubscribe()
        {
            Unsubscribe<ExpenseListChangedMessage>(reloadListToken);
        }

    }
}

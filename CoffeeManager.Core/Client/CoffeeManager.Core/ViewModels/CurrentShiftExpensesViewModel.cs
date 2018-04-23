using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftExpensesViewModel : PageViewModel
    {
        private readonly IPaymentManager manager;
        private List<ExpenseItemViewModel> _items = new List<ExpenseItemViewModel>();
        private MvxSubscriptionToken _token;
        public List<ExpenseItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
          

        public CurrentShiftExpensesViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            _token = MvxMessenger.Subscribe<ExpenseDeletedMessage>(async (obj) => await Initialize());
        }

        protected override async Task DoLoadDataImplAsync()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetShiftExpenses();
                Items = items.Select(s => new ExpenseItemViewModel(manager, s)).ToList();
            });
        }

     
        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<ExpenseDeletedMessage>(_token);
        }
    }
}

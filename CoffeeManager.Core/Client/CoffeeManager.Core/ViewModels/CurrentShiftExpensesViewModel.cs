using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftExpensesViewModel : ViewModelBase
    {
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

        public CurrentShiftExpensesViewModel()
        {
            _token = Subscribe<ExpenseDeletedMessage>(async (obj) => await LoadData());
        }

        public async void Init()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var manager = new PaymentManager();
            var items = await manager.GetShiftExpenses();
            Items = items.Select(s => new ExpenseItemViewModel(s)).ToList();
        }
    }
}

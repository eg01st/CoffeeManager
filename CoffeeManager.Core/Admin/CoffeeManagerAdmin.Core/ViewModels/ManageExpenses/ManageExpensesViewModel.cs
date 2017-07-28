using System;
using CoffeeManagerAdmin.Core.Managers;
using System.Collections.Generic;
using System.Linq;
namespace CoffeeManagerAdmin.Core
{
    public class ManageExpensesViewModel : ViewModelBase
    {
        PaymentManager pm = new PaymentManager();

        public List<ManageExpenseItemViewModel> Items {get;set;}

        public async void Init()
        {
            var items = await pm.GetExpenseItems();
            Items = items.Select(s => new ManageExpenseItemViewModel(s)).ToList();
            RaisePropertyChanged(nameof(Items));
        }

    }
}

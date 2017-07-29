using System;
using CoffeeManagerAdmin.Core.Managers;
using System.Collections.Generic;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class ManageExpensesViewModel : BaseSearchViewModel<ManageExpenseItemViewModel>
    {
        PaymentManager pm = new PaymentManager();

        public async override Task<List<ManageExpenseItemViewModel>> LoadData()
        {
            var items = await pm.GetExpenseItems();
            return items.Select(s => new ManageExpenseItemViewModel(s)).ToList();
        }
    }
}

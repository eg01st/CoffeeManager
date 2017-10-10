using System;
using CoffeManager.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CoffeeManagerAdmin.Core
{
    public class AddShiftExpenseViewModel : BaseSearchViewModel<AddExpenseItemViewModel>
    {
        readonly IPaymentManager manager;

        public AddShiftExpenseViewModel(IPaymentManager manager)
        {
            this.manager = manager;
        }

        public async override Task<List<AddExpenseItemViewModel>> LoadData()
        {
            var items = await manager.GetExpenseItems();
            return items.Select(s => new AddExpenseItemViewModel(s)).ToList();
        }
    }
}

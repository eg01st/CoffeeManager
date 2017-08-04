using System.Collections.Generic;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class ManageExpensesViewModel : BaseSearchViewModel<ManageExpenseItemViewModel>
    {
        private readonly IPaymentManager manager;

        public ManageExpensesViewModel(IPaymentManager manager)
        {
            this.manager = manager;
        }

        public async override Task<List<ManageExpenseItemViewModel>> LoadData()
        {
            var items = await manager.GetExpenseItems();
            return items.Select(s => new ManageExpenseItemViewModel(manager, s)).ToList();
        }
    }
}

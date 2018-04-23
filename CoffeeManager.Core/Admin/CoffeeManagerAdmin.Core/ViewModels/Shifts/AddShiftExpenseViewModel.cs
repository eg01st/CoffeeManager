using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
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

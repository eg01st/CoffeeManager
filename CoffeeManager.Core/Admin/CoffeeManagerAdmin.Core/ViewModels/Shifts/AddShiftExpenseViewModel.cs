using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class AddShiftExpenseViewModel : BaseAdminSearchViewModel<AddExpenseItemViewModel>
    {
        readonly IPaymentManager manager;

        public AddShiftExpenseViewModel(IPaymentManager manager)
        {
            this.manager = manager;
        }

        public override async Task<List<AddExpenseItemViewModel>> LoadData()
        {
            var items = await manager.GetExpenseItems();
            return items.Select(s => new AddExpenseItemViewModel(s)).ToList();
        }
    }
}

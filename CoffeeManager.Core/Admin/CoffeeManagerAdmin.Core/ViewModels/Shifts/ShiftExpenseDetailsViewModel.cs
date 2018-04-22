using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftExpenseDetailsViewModel : ViewModelBase, IMvxViewModel<int>
    {
        private int expenseId;
        readonly IPaymentManager manager;

        public List<SupliedProduct> Items { get; set; }

        public ShiftExpenseDetailsViewModel(IPaymentManager manager)
        {
            this.manager = manager;
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetExpenseDetails(expenseId);
                Items = items.ToList();
                RaisePropertyChanged(nameof(Items));
            });
        }

        public void Prepare(int parameter)
        {
            expenseId = parameter;
        }
    }
}

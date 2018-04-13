using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftExpenseDetailsViewModel : ViewModelBase
    {
        readonly IPaymentManager manager;

        public List<SupliedProduct> Items { get; set; }

        public ShiftExpenseDetailsViewModel(IPaymentManager manager)
        {
            this.manager = manager;
        }

        public async Task Init(int id)
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetExpenseDetails(id);
                Items = items.ToList();
                RaisePropertyChanged(nameof(Items));
            });
        }
    }
}

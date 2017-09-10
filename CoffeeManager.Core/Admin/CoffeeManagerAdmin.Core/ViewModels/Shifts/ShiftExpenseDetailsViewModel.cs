using System;
using CoffeManager.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
namespace CoffeeManagerAdmin.Core
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
            var items = await manager.GetExpenseDetails(id);
            Items = items.ToList();
            RaisePropertyChanged(nameof(Items));
        }
    }
}

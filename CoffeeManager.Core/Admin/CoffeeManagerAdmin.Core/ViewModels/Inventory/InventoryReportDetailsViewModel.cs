using System;
using CoffeManager.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace CoffeeManagerAdmin.Core
{
    public class InventoryReportDetailsViewModel : ViewModelBase
    {
        readonly IInventoryManager manager;

        public List<InventoryReportDetailsItemViewModel> Items { get; set; }

        public InventoryReportDetailsViewModel(IInventoryManager manager)
        {
            this.manager = manager;
        }

        public async Task Init(int id)
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetInventoryReportDetails(id);
                Items = items.Select(s => new InventoryReportDetailsItemViewModel(s)).ToList();
                RaisePropertyChanged(nameof(Items));
            });
        }
    }
}

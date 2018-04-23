using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Inventory
{
    public class InventoryReportDetailsViewModel : ViewModelBase, IMvxViewModel<int>
    {
        private int reportId;
        readonly IInventoryManager manager;

        public List<InventoryReportDetailsItemViewModel> Items { get; set; }

        public InventoryReportDetailsViewModel(IInventoryManager manager)
        {
            this.manager = manager;
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetInventoryReportDetails(reportId);
                Items = items.Select(s => new InventoryReportDetailsItemViewModel(s)).ToList();
                RaisePropertyChanged(nameof(Items));
            });
        }

        public void Prepare(int parameter)
        {
            reportId = parameter;
        }
    }
}

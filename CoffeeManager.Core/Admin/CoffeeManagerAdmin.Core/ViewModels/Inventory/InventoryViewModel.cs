using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Inventory.Create;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Inventory
{
    public class InventoryViewModel : ViewModelBase
    {
        readonly IInventoryManager manager;

        public List<InventoryItemViewModel> Items { get; set; }

        public ICommand CreateReportCommand { get; }

        public InventoryViewModel(IInventoryManager manager)
        {
            this.manager = manager;
            CreateReportCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CreateInventoryViewModel>());
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetInventoryReports();
                Items = items.Select(s => new InventoryItemViewModel(s)).OrderByDescending(o => o.Id).ToList();
                RaisePropertyChanged(nameof(Items));
            });
        }
    }
}

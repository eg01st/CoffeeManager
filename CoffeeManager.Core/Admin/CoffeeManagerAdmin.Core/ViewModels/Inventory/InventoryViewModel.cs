using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Inventory.Create;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Inventory
{
    public class InventoryViewModel : FeedViewModel<InventoryItemViewModel>
    {
        readonly IInventoryManager manager;

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
                ItemsCollection.ReplaceWith(items.Select(s => new InventoryItemViewModel(s)).OrderByDescending(o => o.Id));
            });
        }
    }
}

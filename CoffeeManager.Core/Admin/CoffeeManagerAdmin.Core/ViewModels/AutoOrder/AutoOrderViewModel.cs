using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AutoOrderViewModel : FeedViewModel<AutoOrderItemViewModel>
    {
        private readonly IAutoOrderManager manager;
        public ICommand AddNewAutoOrderCommad { get; }

        public AutoOrderViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
            AddNewAutoOrderCommad = new MvxAsyncCommand(AddNewAutoOrder);
        }

        private async Task AddNewAutoOrder()
        {
            var needToReload = await NavigationService.Navigate<AddAutoOrderViewModel, bool>();
            if (needToReload)
            {
                await RefreshDataAsync();
            }
        }

        protected override async Task<PageContainer<AutoOrderItemViewModel>> GetPageAsync(int skip)
        {
            var items = await ExecuteSafe(async () => await manager.GetAutoOrders());
            return items.Select(s => new AutoOrderItemViewModel(s)).ToPageContainer();
        }

        protected override async Task OnItemSelectedAsync(AutoOrderItemViewModel item)
        {
            bool needToReload = await NavigationService.Navigate<AutoOrderDetailsViewModel, int, bool>(item.Id);
            if (needToReload)
            {
                await RefreshDataAsync();
            }
        }
    }
}
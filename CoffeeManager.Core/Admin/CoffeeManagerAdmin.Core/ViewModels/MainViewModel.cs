using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using CoffeeManagerAdmin.Core.ViewModels.CreditCard;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public async Task ShowInitialViewModelsAsync()
        {
            await Task.WhenAll(GetPageVmTypesForInitialNavigate());
        }

        private IEnumerable<Task> GetPageVmTypesForInitialNavigate()
        {
            yield return NavigationService.Navigate<MoneyViewModel>();
            yield return NavigationService.Navigate<StorageViewModel>();
            yield return NavigationService.Navigate<ManageExpensesViewModel>();
            yield return NavigationService.Navigate<ProductsViewModel>();
            yield return NavigationService.Navigate<StatisticViewModel>();
            yield return NavigationService.Navigate<UsersViewModel>();
            yield return NavigationService.Navigate<SettingsViewModel>();
            yield return NavigationService.Navigate<CreditCardViewModel>();
            yield return NavigationService.Navigate<CategoriesViewModel>();
            yield return NavigationService.Navigate<CoffeeCountersViewModel>();
        }

    }
}

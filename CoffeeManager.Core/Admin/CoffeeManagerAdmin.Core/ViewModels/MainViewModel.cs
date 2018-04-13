﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Home;

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
            yield return NavigationService.Navigate<SideMenuViewModel>();
            yield return NavigationService.Navigate<MoneyViewModel>();
            yield return NavigationService.Navigate<StorageViewModel>();
            yield return NavigationService.Navigate<ManageExpensesViewModel>();
            yield return NavigationService.Navigate<ProductsViewModel>();
            yield return NavigationService.Navigate<StatisticViewModel>();

        }

    }
}

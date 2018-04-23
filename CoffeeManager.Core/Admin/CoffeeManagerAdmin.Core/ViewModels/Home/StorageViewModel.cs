using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Inventory;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using CoffeeManagerAdmin.Core.ViewModels.TransferSuplyProduct;
using CoffeeManagerAdmin.Core.ViewModels.Utilize;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class StorageViewModel : ViewModelBase
    {
        public StorageViewModel()
        {
            ShowSupliedProductsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SuplyProductsViewModel>());
            ShowOrdersCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<OrdersViewModel>());
            ShowInventoryCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<InventoryViewModel>());
            ShowUtilizedSuplyProductsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UtilizeViewModel>());
            TransferSuplyProductsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<TransferSuplyProductsViewModel>());
        }

        public ICommand ShowSupliedProductsCommand { get; }
        public ICommand ShowOrdersCommand { get; }
        public ICommand ShowInventoryCommand { get; }
        public ICommand ShowUtilizedSuplyProductsCommand { get; }
        public ICommand TransferSuplyProductsCommand { get; }
    }
}

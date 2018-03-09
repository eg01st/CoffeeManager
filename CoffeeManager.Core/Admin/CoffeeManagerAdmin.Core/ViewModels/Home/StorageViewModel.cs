using System;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Orders;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class StorageViewModel : ViewModelBase
    {
        public StorageViewModel()
        {
            ShowSupliedProductsCommand = new MvxCommand(() => ShowViewModel<SuplyProductsViewModel>());
            ShowOrdersCommand = new MvxCommand(() => ShowViewModel<OrdersViewModel>());
            ShowInventoryCommand = new MvxCommand(() => ShowViewModel<InventoryViewModel>());
            ShowUtilizedSuplyProductsCommand = new MvxCommand(() => ShowViewModel<UtilizeViewModel>());
            TransferSuplyProductsCommand = new MvxCommand(() => ShowViewModel<TransferSuplyProductsViewModel>());
        }

        public ICommand ShowSupliedProductsCommand { get; }
        public ICommand ShowOrdersCommand { get; }
        public ICommand ShowInventoryCommand { get; }
        public ICommand ShowUtilizedSuplyProductsCommand { get; }
        public ICommand TransferSuplyProductsCommand { get; }
    }
}

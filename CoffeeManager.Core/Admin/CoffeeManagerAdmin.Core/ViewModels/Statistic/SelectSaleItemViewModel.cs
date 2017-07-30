using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class SelectSaleItemViewModel : ListItemViewModelBase
    {
        public bool IsSelected {get;set;}

        public ICommand ToggleIsSelectedCommand {get;set;}

        public SelectSaleItemViewModel(string name)
        {
            Name = name;
            ToggleIsSelectedCommand = new MvxCommand(() => {IsSelected = !IsSelected; RaisePropertyChanged(nameof(IsSelected));});
        }
    }
}

using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class SelectSaleItemViewModel : ViewModelBase
    {
        public string Name {get;set;}
        public bool IsSelected {get;set;}

        public ICommand ToggleIsSelectedCommand {get;set;}

        public SelectSaleItemViewModel(string name)
        {
            Name = name;
            ToggleIsSelectedCommand = new MvxCommand(() => {IsSelected = !IsSelected; RaisePropertyChanged(nameof(IsSelected));});
        }
    }
}

using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public abstract class ListItemViewModelBase : ViewModelBase
    {
        public ICommand GoToDetailsCommand {get;set;}

        public ListItemViewModelBase()
        {
            GoToDetailsCommand = new MvxCommand(DoGoToDetails);
        }

        protected virtual void DoGoToDetails()
        {

        }
    }
}

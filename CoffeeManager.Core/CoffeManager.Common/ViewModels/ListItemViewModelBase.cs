using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace CoffeManager.Common
{
    public abstract class ListItemViewModelBase : ViewModelBase
    {
        public ICommand GoToDetailsCommand {get;set;}

        public virtual string Name {get;set;}

        public ListItemViewModelBase()
        {
            GoToDetailsCommand = new MvxCommand(DoGoToDetails);
        }

        protected virtual void DoGoToDetails()
        {

        }
    }
}

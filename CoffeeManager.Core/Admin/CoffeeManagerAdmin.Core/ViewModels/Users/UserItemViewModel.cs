using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.Managers;
namespace CoffeeManagerAdmin.Core
{
    public class UserItemViewModel : ListItemViewModelBase
    {
        public int Id {get;set;}
        public string UserName {get;set;}
        public bool IsActive {get;set;}

        public ICommand ToggleIsActiveCommand {get;set;}
        
        public UserItemViewModel()
        {
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);

        }

        private async void DoToggleIsActive()
        {
            var um = new UserManager();
            await um.ToggleEnabled(Id);
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<UserDetailsViewModel>(new { id = Id});
        }
   }
}

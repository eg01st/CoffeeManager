using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeManager.Common.Managers;

namespace CoffeeManagerAdmin.Core
{
    public class UserItemViewModel : ListItemViewModelBase
    {
        private readonly IUserManager manager;

        public int Id {get;set;}
        public string UserName {get;set;}
        public bool IsActive {get;set;}

        public ICommand ToggleIsActiveCommand {get;set;}

        public UserItemViewModel(IUserManager manager)
        {
            this.manager = manager;
            ToggleIsActiveCommand = new MvxCommand(DoToggleIsActive);

        }

        private async void DoToggleIsActive()
        {
            await ExecuteSafe(async () => await manager.ToggleEnabled(Id));
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<UserDetailsViewModel>(new { id = Id});
        }
   }
}

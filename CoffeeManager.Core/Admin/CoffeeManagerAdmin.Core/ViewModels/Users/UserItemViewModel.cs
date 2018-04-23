using System.Windows.Input;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Users
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

        protected override async void DoGoToDetails()
        {
            await NavigationService.Navigate<UserDetailsViewModel, int>(Id);
        }
   }
}

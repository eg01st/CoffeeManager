using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using System;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels.CoffeeCounter;
using CoffeeManager.Models.Data.DTO.User;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserManager userManager;
        private readonly IShiftManager shiftManager;
        private UserDTO[] users;

        public ICommand SelectUserCommand { get; }
        public ICommand RefreshCommand { get; }

        public UserDTO[] Users
        {
            get => users;
            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }
        
        public LoginViewModel(IShiftManager shiftManager, IUserManager userManager)
        {
            this.userManager = userManager;
            this.shiftManager = shiftManager;
            SelectUserCommand = new MvxCommand<UserDTO>(DoSelectUser);
            RefreshCommand = new MvxAsyncCommand(DoRefresh);
        }
        
        public override async Task Initialize()
        {
            var res = await ExecuteSafe(userManager.GetUsers);
            Users = res?.Where(u => u.IsActive).ToArray();
        }

        private async Task DoRefresh()
        {
            await ExecuteSafe(async () =>
            {
                await Initialize();
            });
        }

        private async void DoSelectUser(UserDTO user)
        {
            await NavigationService.Navigate<CoffeeCounterViewModel, int>(user.Id);
        }
    }
}

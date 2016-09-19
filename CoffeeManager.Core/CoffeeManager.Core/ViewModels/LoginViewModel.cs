using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private UserManager userManager;
        private ShiftManager shiftManager; 
        private ICommand selectUserCommand;
        private User[] users;

        public ICommand SelectUserCommand => selectUserCommand;

        public LoginViewModel()
        {
            userManager = new UserManager();
            shiftManager = new ShiftManager();
            selectUserCommand = new MvxCommand<User>(DoSelectUser);
        }

        private void DoSelectUser(User user)
        {
            shiftManager.StartUserShift(user.Id);
            ShowViewModel<MainViewModel>();
        }

        public User[] Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public void Init()
        {
            Users = userManager.GetUsers();
        }
    }
}

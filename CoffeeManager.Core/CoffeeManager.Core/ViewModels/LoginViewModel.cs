using System;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserManager _userManager;
        private readonly ShiftManager _shiftManager;
        private readonly ICommand _selectUserCommand;
        private User[] _users;

        public ICommand SelectUserCommand => _selectUserCommand;

        public LoginViewModel()
        {
            _userManager = new UserManager();
            _shiftManager = new ShiftManager();
            _selectUserCommand = new MvxCommand<User>(DoSelectUser);
        }

        private async void DoSelectUser(User user)
        {
            UserDialogs.Prompt(new PromptConfig()
            {
                Message = "Введите показание счетчика на кофемолке",
                InputType = InputType.Number,
                OnAction = async (obj) =>
                {
                    if (obj.Ok)
                    {
                        int shiftId = await _shiftManager.StartUserShift(user.Id, int.Parse(obj.Text));
                        ShowViewModel<MainViewModel>(new { userId = user.Id, shiftId = shiftId });
                    }
                }
            });
        }

        public User[] Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }

        public async void Init()
        {
            Shift currentShift = await _shiftManager.GetCurrentShift();
            if (currentShift != null)
            {
                ShowViewModel<MainViewModel>(new { userId = currentShift.UserId, shiftId = currentShift.Id });
            }
            Users = await _userManager.GetUsers();
        }
    }
}

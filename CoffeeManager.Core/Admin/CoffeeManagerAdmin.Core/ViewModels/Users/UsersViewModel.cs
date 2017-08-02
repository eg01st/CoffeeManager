using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.Managers;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class UsersViewModel : ViewModelBase
    {
        UserManager manager = new UserManager();
       
        private List<UserItemViewModel> users;
        private ICommand _addUserCommand;

        public List<UserItemViewModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }


        public async void Init()
        {
            await TryCatchSpecifics(async () => 
            {
                var items = await manager.GetUsers();
                Users = items.Select(s => new UserItemViewModel{UserName = s.Name, IsActive = s.IsActive, Id = s.Id})
                    .OrderByDescending(o => o.IsActive).ToList();
            });
        }

        public ICommand AddUserCommand => _addUserCommand;

        public UsersViewModel()
        {
            _addUserCommand = new MvxCommand(DoAddUser);
        }

        private void DoAddUser()
        {
            ShowViewModel<UserDetailsViewModel>();
        }
    }
}

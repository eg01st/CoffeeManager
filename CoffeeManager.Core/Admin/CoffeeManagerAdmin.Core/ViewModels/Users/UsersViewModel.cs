﻿using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class UsersViewModel : ViewModelBase
    {
        private MvxSubscriptionToken refreshUsersToken;

        private readonly IUserManager manager;

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


        public async Task Init()
        {
            await ExecuteSafe(async () => 
            {
                var items = await manager.GetUsers();
                Users = items.Select(s => new UserItemViewModel(manager){UserName = s.Name, IsActive = s.IsActive, Id = s.Id})
                    .OrderByDescending(o => o.IsActive).ToList();
            });
        }

        public ICommand AddUserCommand => _addUserCommand;
              

        public UsersViewModel(IUserManager manager)
        {
            this.manager = manager;
            _addUserCommand = new MvxCommand(DoAddUser);

            refreshUsersToken = Subscribe<RefreshUserListMessage>(async (obj) => await Init());
        }

        private void DoAddUser()
        {
            ShowViewModel<UserDetailsViewModel>();
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            Unsubscribe<RefreshUserListMessage>(refreshUsersToken);
        }
    }
}

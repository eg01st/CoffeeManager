using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
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

        public int AmountToPay { get; set; }

        public async Task Init()
        {
            await ExecuteSafe(async () => 
            {
                AmountToPay = await manager.GetSalaryAmountToPay();
                RaisePropertyChanged(nameof(AmountToPay));

                var items = await manager.GetUsers();
                Users = items.Select(s => new UserItemViewModel(manager){UserName = s.Name, IsActive = s.IsActive, Id = s.Id})
                    .OrderByDescending(o => o.IsActive).ToList();
            });
        }

        public ICommand AddUserCommand => _addUserCommand;
              
        public MvxAsyncCommand<UserItemViewModel> ItemSelectedCommand { get; }

        public UsersViewModel(IUserManager manager)
        {
            this.manager = manager;
            _addUserCommand = new MvxCommand(DoAddUser);
            ItemSelectedCommand = new MvxAsyncCommand<UserItemViewModel>(OnItemSelectedAsync);

            refreshUsersToken = Subscribe<RefreshUserListMessage>(async (obj) => await Init());
        }
        
        private async Task OnItemSelectedAsync(UserItemViewModel item)
        {
            item.ThrowIfNull(nameof(item));
            
            item.SelectCommand.Execute();

            await Task.Yield();
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

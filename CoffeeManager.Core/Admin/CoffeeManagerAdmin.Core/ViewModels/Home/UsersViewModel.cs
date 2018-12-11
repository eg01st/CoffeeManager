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
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class UsersViewModel : FeedViewModel<UserItemViewModel>
    {
        private MvxSubscriptionToken refreshUsersToken;

        private readonly IUserManager manager;
        private ICommand _addUserCommand;

        public int AmountToPay { get; set; }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () => 
            {
                AmountToPay = await manager.GetSalaryAmountToPay();
                RaisePropertyChanged(nameof(AmountToPay));

                var items = await manager.GetUsers();
                ItemsCollection.ReplaceWith(items.Select(s => new UserItemViewModel(manager){UserName = s.Name, IsActive = s.IsActive, Id = s.Id})
                    .OrderByDescending(o => o.IsActive));
            });
        }

        public ICommand AddUserCommand => _addUserCommand;
              

        public UsersViewModel(IUserManager manager)
        {
            this.manager = manager;
            _addUserCommand = new MvxAsyncCommand(DoAddUser);
            refreshUsersToken = MvxMessenger.Subscribe<RefreshUserListMessage>(async (obj) => await Initialize());
        }

        private async Task DoAddUser()
        {
            await NavigationService.Navigate<UserDetailsViewModel>();
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<RefreshUserListMessage>(refreshUsersToken);
        }
    }
}

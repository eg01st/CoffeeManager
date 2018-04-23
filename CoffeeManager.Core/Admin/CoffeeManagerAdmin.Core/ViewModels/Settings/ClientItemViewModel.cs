using CoffeeManager.Models;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Settings
{
    public class ClientItemViewModel : ListItemViewModelBase
    {
        private readonly UserAcount account;
        private string userId;
        private string apiUrl;

        public ClientItemViewModel(UserAcount account)
        {
            this.account = account;
            userId = account.Id;
            Name = account.Email;
            apiUrl = account.ApiUrl;
        }

        protected override async void DoGoToDetails()
        {
           await NavigationService.Navigate<ClientDetailsViewModel,UserAcount>(account);
        }
    }
}

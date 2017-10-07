using System;
using CoffeManager.Common;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class ClientItemViewModel : ListItemViewModelBase
    {
        private string userId;
        private string apiUrl;

        public ClientItemViewModel(UserAcount account)
        {
            userId = account.Id;
            Name = account.Email;
            apiUrl = account.ApiUrl;
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<ClientDetailsViewModel>(new {id = userId, name = Name, apiUrl = apiUrl});
        }
    }
}

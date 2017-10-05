using System;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class ClientDetailsViewModel : ViewModelBase
    {
        private string id;
        private string apiUrl;

        public string Email { get; set; }

        public string ApiUrl 
        {
            get { return apiUrl; }
            set
            {
                apiUrl = value;
                RaisePropertyChanged(nameof(ApiUrl));
            }
        }

        public ICommand DeleteUserCommand { get; set; }

        readonly IAccountManager manager;
        readonly ILocalStorage localStorage;

        public ClientDetailsViewModel(IAccountManager manager, ILocalStorage localStorage)
        {
            this.localStorage = localStorage;
            this.manager = manager;
            DeleteUserCommand = new MvxCommand(DoDeleteUser);
        }

        private void DoDeleteUser()
        {
            Confirm("Удалить пользователя?", DeleteUser);
        }

        private async Task DeleteUser()
        {
            await manager.DeleteAdminUser(id, Email, ApiUrl);
            Publish(new RefreshAdminUsersMessage(this));
            CloseCommand.Execute(null);
        }

        public void Init(string id, string name, string apiUrl)
        {
            Email = name;
            this.id = id;
            ApiUrl = apiUrl;
            RaiseAllPropertiesChanged();
        }

    }
}

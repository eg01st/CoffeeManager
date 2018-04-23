using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Settings
{
    public class ClientDetailsViewModel : ViewModelBase, IMvxViewModel<UserAcount>
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

        public void Prepare(UserAcount parameter)
        {
            Email = parameter.Email;
            this.id = parameter.Id;
            ApiUrl = parameter.ApiUrl;
            RaiseAllPropertiesChanged();
        }
    }
}

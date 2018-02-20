using System;
using CoffeManager.Common;
using System.Windows.Input;
using CoffeManager.Common.Managers;
using MvvmCross.Core.ViewModels;
using System.Diagnostics;
namespace CoffeeManagerAdmin.Core
{
    public class CreateClientViewModel : ViewModelBase
    {
        private string email;
        private string password;
        private string confirmPassword;
        private string apiUrl;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string ApiUrl
        {
            get { return apiUrl; }
            set
            {
                apiUrl = value;
                RaisePropertyChanged(nameof(ApiUrl));
            }
        }

        public ICommand RegisterUserCommand { get; set; }

        readonly IAccountManager manager;

        public CreateClientViewModel(IAccountManager manager)
        {
            this.manager = manager;
            RegisterUserCommand = new MvxCommand(DoRegisterUser);
        }

        private async void DoRegisterUser()
        {
            if (Password != ConfirmPassword)
            {
                Alert("Пароли не совпадают!");
                return;
            }
            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                Alert("Введите ссылку на сервис");
                return;
            }
            await ExecuteSafe(async () =>
            {
                try
                {
                    await manager.Register(Email, Password, apiUrl);
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains(" is already taken."))
                    {
                        Alert("Имейл занят!");
                        return;
                    }
                    Debug.WriteLine(ex.ToDiagnosticString());
                    Alert("Сервис не отвечает!");
                    return;
                }

                Publish(new RefreshAdminUsersMessage(this));
                CloseCommand.Execute(null);
            });

        }
    }
}

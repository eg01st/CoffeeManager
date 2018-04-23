using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace CoffeManager.Common.ViewModels
{
    public abstract class ViewModelBase : PageViewModel
    {
        private IAccountManager AccountManager
        {
            get
            {
                return Mvx.Resolve<IAccountManager>();
            }
        }

        private ILocalStorage LocalStorage
        {
            get
            {
                return Mvx.Resolve<ILocalStorage>();
            }
        }
    
        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);
            DoUnsubscribe();
        }


        protected MvxSubscriptionToken Subscribe<TMessage>(Action<TMessage> action)
            where TMessage : MvxMessage
        {
            return MvxMessenger.Subscribe<TMessage>(action, MvxReference.Weak);
        }

        protected void Unsubscribe<TMessage>(MvxSubscriptionToken id)
            where TMessage : MvxMessage
        {
            MvxMessenger.Unsubscribe<TMessage>(id);
        }

        protected void Publish<T>(T message) where T : MvxMessage
        {
            MvxMessenger.Publish(message);
        }

   

        public void ShowSuccessMessage(string message)
        {
            UserDialogs.ShowSuccess(message, 300);
        }

   
        protected async Task<bool> PromtLogin()
        {
            var email = await PromtStringAsync("Введите логин");
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var password = await PromtStringAsync("Введите пароль", InputType.Password);
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            await AccountManager.Authorize(email, password);
            LocalStorage.SetUserInfo(new UserInfo() { Login = email, Password = password });
            return true;
        }
    }
}

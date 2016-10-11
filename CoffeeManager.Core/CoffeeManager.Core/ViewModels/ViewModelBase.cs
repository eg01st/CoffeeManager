using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class ViewModelBase : MvxViewModel
    {
       

        protected ProductManager ProductManager = new ProductManager();

        protected IMvxMessenger MvxMessenger
        {
            get
            {
                return Mvx.Resolve<IMvxMessenger>();
            }
        }

        protected IUserDialogs UserDialogs
        {
            get
            {
                return Mvx.Resolve<IUserDialogs>();
            }
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
            UserDialogs.ShowSuccess(message, 1000);
        }
    }
}

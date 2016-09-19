using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
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

        public void ToastMessage(string message)
        {
            //InvokeOnMainThread(() =>
            //{
            //    var toast = Mvx.Resolve<IUserDialogs>();
            //    toast.InfoToast(message);

            //});

        }
    }
}

using System.Windows.Input;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeManager.Common.ViewModels
{
    public abstract class ListItemViewModelBase : FeedItemElementViewModel
    {
        public ICommand GoToDetailsCommand {get;set;}

        public virtual string Name {get;set;}

        public ListItemViewModelBase()
        {
            GoToDetailsCommand = new MvxCommand(DoGoToDetails);
        }

        protected virtual void DoGoToDetails()
        {

        }

        protected override void Select()
        {
            DoGoToDetails();
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
    }
}

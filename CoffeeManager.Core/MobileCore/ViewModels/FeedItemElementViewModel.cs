using MvvmCross.Core.ViewModels;

namespace MobileCore.ViewModels
{
    public abstract class FeedItemElementViewModel : SimpleViewModel
    {
        protected FeedItemElementViewModel()
        {
            SelectCommand = new MvxCommand(OnSelect, () => CanSelect);
        }

        public MvxCommand SelectCommand { get; }

        protected virtual bool CanSelect => true;

        private void OnSelect()
        {
            if (!CanSelect)
            {
                return;
            }

            Select();
        }

        protected virtual void Select()
        {
        }
    }
}
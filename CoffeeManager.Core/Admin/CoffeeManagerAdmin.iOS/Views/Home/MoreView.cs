using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.iOS.TableSources;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace CoffeeManagerAdmin.iOS.Views.Home
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Еще",
                        TabIconName = "ic_more_horiz.png",
                        TabSelectedIconName = "ic_more_horiz.png")]
    public partial class MoreView : ViewControllerBase<MoreViewModel>
    {
        protected override bool UseCustomBackButton => false;
        
        public MoreView() : base("MoreView", null)
        {
        }

        protected override void DoBind()
        {
            var source = new SimpleTableSource(ItemsTableSource, MoreItemViewCell.Key, MoreItemViewCell.Nib);
            ItemsTableSource.Source = source;

            var set = this.CreateBindingSet<MoreView, MoreViewModel>();
            set.Bind(source).To(vm => vm.ItemsCollection);
            set.Apply();
        }
    }
}


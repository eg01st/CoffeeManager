using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Счетчики кофемолок",
    TabIconName = "ic_attach_money.png",
    TabSelectedIconName = "ic_attach_money.png")]
    public partial class CoffeeCountersView : ViewControllerBase<CoffeeCountersViewModel>
    {
        protected override bool UseCustomBackButton => false;

        private SimpleTableSource tableSource;
        
        public CoffeeCountersView() : base("CoffeeCountersView", null)
        {
        }
        
        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            var addCounterButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(addCounterButton, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {addCounterButton, "Clicked AddCounterCommand"},
            });
        }

        protected override void InitStylesAndContent()
        {
            Title = "Счетчики кофемолок";
            tableSource = new SimpleTableSource(CountersTableView, CounterTableViewCell.Key, CounterTableViewCell.Nib);
            CountersTableView.Source = tableSource;
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<CoffeeCountersView, CoffeeCountersViewModel>();
            set.Bind(tableSource).To(vm => vm.ItemsCollection);
            set.Bind(tableSource).For(d => d.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();
        }
    }
}


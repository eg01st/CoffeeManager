using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectCalculationListView : MvxViewController
    {
        public SelectCalculationListView() : base("SelectCalculationListView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            var source = new SimpleTableSource(TableView, SelectCalculationItemViewCell.Key, SelectCalculationItemViewCell.Nib);
            TableView.Source = source;
            var set = this.CreateBindingSet<SelectCalculationListView, SelectCalculationListViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Calculation;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoffeeManagerAdmin.iOS.Views.Cells;
using CoffeManager.Common;
using MobileCore.iOS.ViewControllers;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CalculationView : ViewControllerBase<CalculationViewModel>
    {
        public CalculationView() : base("CalculationView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Калькуляция";

            var source = new SimpleTableSource(TableView, CalculationItemViewCell.Key, CalculationItemViewCell.Nib);
            TableView.Source = source;

            var set = this.CreateBindingSet<CalculationView, CalculationViewModel>();
            set.Bind(NameLabel).To(vm => vm.Name);
            set.Bind(AddProductButton).To(vm => vm.AddItemCommand);
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


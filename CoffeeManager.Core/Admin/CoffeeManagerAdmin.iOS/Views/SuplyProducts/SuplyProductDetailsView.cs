using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductDetailsView : MvxViewController
    {
        public SuplyProductDetailsView() : base("SuplyProductDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Детали товара";
            var set = this.CreateBindingSet<SuplyProductDetailsView, SuplyProductDetailsViewModel>();
            set.Bind(NameText).To(vm => vm.Name);
            set.Bind(ExpenseNumerationNameTextField).To(vm => vm.ExpenseNumerationName);
            set.Bind(SuplyPriceText).To(vm => vm.SupliedPrice).WithConversion(new DecimalToStringConverter());
            set.Bind(SalePriceLabel).To(vm => vm.SalePrice).WithConversion(new DecimalToStringConverter());
            set.Bind(ItemCountText).To(vm => vm.ItemCount).WithConversion(new DecimalToStringConverter());
            set.Bind(ExpenseNumerationMultyplierTextField).To(vm => vm.ExpenseNumerationMultiplier);
            set.Bind(SaveButton).To(vm => vm.SaveCommand);
            set.Bind(DeleteButton).To(vm => vm.DeleteCommand);
            set.Bind(InventoryNeededSwitch).For(s => s.On).To(vm => vm.InventoryEnabled);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


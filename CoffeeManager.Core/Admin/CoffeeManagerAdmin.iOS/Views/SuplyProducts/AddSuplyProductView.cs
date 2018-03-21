using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddSuplyProductView : ViewControllerBase<AddSuplyProductViewModel>
    {
        public AddSuplyProductView() : base("AddSuplyProductView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Создать продукт";

            var set = this.CreateBindingSet<AddSuplyProductView, AddSuplyProductViewModel>();
            set.Bind(SuplyProductTextView).To(vm => vm.Name);
            set.Bind(AddButton).To(vm => vm.AddSuplyProductCommand);
            set.Apply();
        }

    }
}


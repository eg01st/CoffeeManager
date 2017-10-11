using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class StorageView : ViewControllerBase<StorageViewModel>
    {
        protected override bool UseCustomBackButton => false;

        public StorageView() : base("StorageView", null)
        {
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<StorageView, StorageViewModel>();
            set.Bind(SuplyProductsButton).To(vm => vm.ShowSupliedProductsCommand);
            set.Bind(OrdersButton).To(vm => vm.ShowOrdersCommand);
            set.Bind(InventoryButton).To(vm => vm.ShowInventoryCommand);
            set.Bind(UtilizeButton).To(vm => vm.ShowUtilizedSuplyProductsCommand);
            set.Apply();
        }
    }
}


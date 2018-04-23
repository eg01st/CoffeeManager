﻿using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Склад",
                        TabIconName = "ic_shopping_basket.png",
                        TabSelectedIconName = "ic_shopping_basket.png")]
    public partial class StorageView : ViewControllerBase<StorageViewModel>
    {
        protected override bool UseCustomBackButton => false;

        public StorageView() : base("StorageView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            Title = "Склад";

            var transferButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_swap_horiz"),
                Title = "Переводы"
            };

            this.AddBindings(new Dictionary<object, string>
            {
                {transferButton, "Clicked TransferSuplyProductsCommand"},
            });

            NavigationItem.SetRightBarButtonItem(transferButton, true);
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


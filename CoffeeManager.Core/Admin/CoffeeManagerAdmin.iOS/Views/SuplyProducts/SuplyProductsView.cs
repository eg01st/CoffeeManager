﻿using CoffeeManagerAdmin.Core.ViewModels;
using UIKit;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
 using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
 using CoffeeManagerAdmin.iOS.Extensions;
 using CoffeeManagerAdmin.iOS.TableSources;
 using CoffeeManagerAdmin.iOS.Views.Abstract;
 using CoffeManager.Common;
 using CoffeManager.Common.ViewModels;
 using MvvmCross.Binding.iOS.Views;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SuplyProductsView : SearchViewController<SuplyProductsView, SuplyProductsViewModel, ListItemViewModelBase>
    {
        protected override SimpleTableSource TableSource => new SuplyProductTableSource(TableView, SuplyProductCell.Key, SuplyProductCell.Nib, SuplyProductHeaderCell.Key, SuplyProductHeaderCell.Nib);

        protected override UIView TableViewContainer => ContainerView;

        private MvxPickerViewModel coffeeRoomPickerViewModel;
        
        public SuplyProductsView() : base("SuplyProductsView", null)
        {
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            Title = "Баланс продуктов";

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddNewSuplyProductCommand"},
            });
        }

        protected override void DoViewDidLoad()
        {
            var toolbar = Helper.ProducePickerToolbar(View);

            coffeeRoomPickerViewModel = Helper.ProducePicker(CoffeeRoomTextField, toolbar);
        }

        protected override MvxFluentBindingDescriptionSet<SuplyProductsView, SuplyProductsViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<SuplyProductsView, SuplyProductsViewModel>();
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<SuplyProductsView, SuplyProductsViewModel>();
            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);
            set.Bind(CoffeeRoomTextField).To(vm => vm.CurrentCoffeeRoomName);

            set.Apply();
        }
    }
}


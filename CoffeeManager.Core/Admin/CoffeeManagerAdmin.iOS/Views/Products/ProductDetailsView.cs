﻿using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Products;
using CoffeeManagerAdmin.iOS.Extensions;
using CoffeeManagerAdmin.iOS.Views.Controls;
using CoreGraphics;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
    public partial class ProductDetailsView : ViewControllerBase<ProductDetailsViewModel>
    {
        public ProductDetailsView() : base("ProductDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Детали товара";
            var toolbar = Helper.ProducePickerToolbar(View);

            var cupPickerViewModel = Helper.ProducePicker(CupTypeCategoryText, toolbar);
            var productTypePickerViewModel =  Helper.ProducePicker(ProductTypeText, toolbar);
            
            var picker = new ColoredPickerView();
            var viewModel = new MvxPickerViewModel(picker);
            picker.Model = viewModel;
            picker.ShowSelectionIndicator = true;
            SelectedColorTextField.InputView = picker;
            SelectedColorTextField.InputAccessoryView = toolbar;

            
            var set = this.CreateBindingSet<ProductDetailsView, ProductDetailsViewModel>();
            set.Bind(NameText).To(vm => vm.Name);
            set.Bind(PriceText).To(vm => vm.Price);
            set.Bind(PolicePriceText).To(vm => vm.PolicePrice);
            set.Bind(PriceTitleLabel).To(vm => vm.PriceTitle);
            set.Bind(PolicePriceTitleLabel).To(vm => vm.PolicePriceTitle);
            set.Bind(CupTypeCategoryText).To(vm => vm.CupTypeName);
            set.Bind(ProductTypeText).To(vm => vm.ProductTypeName);
            set.Bind(AddProductButton).To(vm => vm.AddProductCommand);
            set.Bind(AddProductButton).For(b => b.Enabled).To(vm => vm.IsAddEnabled);
            set.Bind(AddProductButton).For("Title").To(vm => vm.ButtonTitle);
            set.Bind(cupPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CupTypesList);
            set.Bind(cupPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCupType);
            set.Bind(productTypePickerViewModel).For(p => p.ItemsSource).To(vm => vm.CategoriesList);
            set.Bind(productTypePickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedProductType);
            set.Bind(viewModel).For(p => p.ItemsSource).To(vm => vm.Colors);
            set.Bind(viewModel).For(p => p.SelectedItem).To(vm => vm.SelectedColor);
            set.Bind(SelectedColorTextField).For(c => c.BackgroundColor).To(vm => vm.SelectedColor).WithConversion(
                new GenericConverter<string, UIColor>((arg) => ColorExtensions.ParseColorFromHex(arg)));
            set.Bind(IsSaleByWeightSwitch).For(s => s.On).To(vm => vm.IsSaleByWeight);
            set.Apply();

            var btn = new UIBarButtonItem()
            {
                Title = "Калькуляция"
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked SelectCalculationItemsCommand"},

            });

        }
    }
}


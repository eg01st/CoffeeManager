using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Products;
using CoffeeManagerAdmin.iOS.Extensions;
using CoffeeManagerAdmin.iOS.TableSources;
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

            StickBottomButtonToKeyboard(BottomButtonContraint);

            Title = "Детали товара";
            var toolbar = Helper.ProducePickerToolbar(View);

            var cupPickerViewModel = Helper.ProducePicker(CupTypeCategoryText, toolbar);
            var categoryPickerViewModel =  Helper.ProducePicker(ProductTypeText, toolbar);
            
            var picker = new ColoredPickerView();
            var colorPickerViewModel = new MvxPickerViewModel(picker);
            picker.Model = colorPickerViewModel;
            picker.ShowSelectionIndicator = true;
            SelectedColorTextField.InputView = picker;
            SelectedColorTextField.InputAccessoryView = toolbar;

            var source = new SimpleTableSource(PaymentStrategyTableView,
                PaymentStrategyTableViewCell.Key,
                PaymentStrategyTableViewCell.Nib,
                PaymentStrategyTableViewHeader.Key,
                PaymentStrategyTableViewHeader.Nib);
            PaymentStrategyTableView.Source = source;
  
            var set = this.CreateBindingSet<ProductDetailsView, ProductDetailsViewModel>();
            set.Bind(NameText).To(vm => vm.Name);
            set.Bind(PriceText).To(vm => vm.Price);
            set.Bind(PolicePriceText).To(vm => vm.PolicePrice);
            set.Bind(PriceTitleLabel).To(vm => vm.PriceTitle);
            set.Bind(PolicePriceTitleLabel).To(vm => vm.PolicePriceTitle);
            set.Bind(CupTypeCategoryText).To(vm => vm.CupTypeName);
            set.Bind(ProductTypeText).To(vm => vm.CategoryName);
            set.Bind(AddProductButton).To(vm => vm.AddProductCommand);
            set.Bind(AddProductButton).For(b => b.Enabled).To(vm => vm.IsAddEnabled);
            set.Bind(AddProductButton).For("Title").To(vm => vm.ButtonTitle);
            set.Bind(cupPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CupTypesList);
            set.Bind(cupPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCupType);
            set.Bind(categoryPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CategoriesList);
            set.Bind(categoryPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCategory);
            set.Bind(colorPickerViewModel).For(p => p.ItemsSource).To(vm => vm.Colors);
            set.Bind(colorPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedColor);
            set.Bind(SelectedColorTextField).For(c => c.BackgroundColor).To(vm => vm.SelectedColor).WithConversion(
                new GenericConverter<string, UIColor>((arg) => ColorExtensions.ParseColorFromHex(arg)));
            set.Bind(IsSaleByWeightSwitch).For(s => s.On).To(vm => vm.IsSaleByWeight);
            set.Bind(DescriptionTextField).To(vm => vm.Description);
            set.Bind(source).To(vm => vm.ItemsCollection);
            set.Bind(AddPaymentStrategyButton).To(vm => vm.AddPaymentStrategyCommand);
            set.Bind(IsPaymentPercentStrategySwich).For(s => s.On).To(vm => vm.IsPercentPaymentEnabled);
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


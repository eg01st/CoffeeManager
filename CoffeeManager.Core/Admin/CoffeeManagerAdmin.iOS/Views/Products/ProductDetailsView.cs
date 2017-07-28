

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using CoreGraphics;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ProductDetailsView : MvxViewController
    {
        public ProductDetailsView() : base("ProductDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Детали продукта";
            var toolbar = new UIToolbar(new CGRect(0, 0, this.View.Frame.Width, 44));
            toolbar.UserInteractionEnabled = true;
            toolbar.BarStyle = UIBarStyle.BlackOpaque;
            var doneButton = new UIBarButtonItem();
            doneButton.Title = "Готово";
            doneButton.Style = UIBarButtonItemStyle.Bordered;
            doneButton.TintColor = UIColor.Black;
            doneButton.Clicked += (sender, e) =>
            {
                View.EndEditing(true);
            };
            toolbar.SetItems(new[] { doneButton }, false);


            var cupTypePicker = new UIPickerView();
            var cupPickerViewModel = new MvxPickerViewModel(cupTypePicker);
            cupTypePicker.Model = cupPickerViewModel;
            cupTypePicker.ShowSelectionIndicator = true;
            CupTypeCategoryText.InputView = cupTypePicker;
            CupTypeCategoryText.InputAccessoryView = toolbar;


            var productTypePicker = new UIPickerView();
            var productTypePickerViewModel = new MvxPickerViewModel(productTypePicker);
            productTypePicker.Model = productTypePickerViewModel;
            productTypePicker.ShowSelectionIndicator = true;
            ProductTypeText.InputView = productTypePicker;
            ProductTypeText.InputAccessoryView = toolbar;

            var set = this.CreateBindingSet<ProductDetailsView, ProductDetailsViewModel>();
            set.Bind(NameText).To(vm => vm.Name);
            set.Bind(PriceText).To(vm => vm.Price);
            set.Bind(PolicePriceText).To(vm => vm.PolicePrice);
            set.Bind(CupTypeCategoryText).To(vm => vm.CupTypeName);
            set.Bind(ProductTypeText).To(vm => vm.ProductTypeName);
            set.Bind(AddProductButton).To(vm => vm.AddProductCommand);
            set.Bind(AddProductButton).For(b => b.Enabled).To(vm => vm.IsAddEnabled);
             set.Bind(AddProductButton).For("Title").To(vm => vm.ButtonTitle);
            set.Bind(cupPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CupTypesList);
            set.Bind(cupPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCupType);
            set.Bind(productTypePickerViewModel).For(p => p.ItemsSource).To(vm => vm.ProductTypesList);
            set.Bind(productTypePickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedProductType);
            set.Apply();

        }
    }
}


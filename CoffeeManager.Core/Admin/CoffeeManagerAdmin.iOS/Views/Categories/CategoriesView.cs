using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.iOS.TableSources;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoreGraphics;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    public partial class CategoriesView : ViewControllerBase<CategoriesViewModel>
    {
        private SimpleTableSource datasource;

        private MvxPickerViewModel coffeeRoomPickerViewModel;

        public CategoriesView() : base("CategoriesView", null)
        {
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            var addCategoryButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(addCategoryButton, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {addCategoryButton, "Clicked AddCategoryCommand"},
            });
        }

        protected override void InitStylesAndContent()
        {
            Title = "Категории товаров";
            datasource = new SimpleTableSource(CategoriesTableView, CategoryTableViewCell.Key, CategoryTableViewCell.Nib);
            CategoriesTableView.Source = datasource;

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

            var coffeeRoomPicker = new UIPickerView();
            coffeeRoomPickerViewModel = new MvxPickerViewModel(coffeeRoomPicker);
            coffeeRoomPicker.Model = coffeeRoomPickerViewModel;
            coffeeRoomPicker.ShowSelectionIndicator = true;
            CoffeeRoomTextField.InputView = coffeeRoomPicker;
            CoffeeRoomTextField.InputAccessoryView = toolbar;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<CategoriesView, CategoriesViewModel>();
            set.Bind(datasource).To(vm => vm.ItemsCollection);
            set.Bind(datasource).For(d => d.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Bind(CoffeeRoomTextField).To(vm => vm.CurrentCoffeeRoomName);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);
            set.Apply();
        }
    }
}


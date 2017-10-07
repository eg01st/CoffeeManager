using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using CoreGraphics;
using MvvmCross.Binding.iOS.Views;
using CoffeManager.Common;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class MainView : ViewControllerBase<MainViewModel>
    {
        public MainView() : base("MainView", null)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                ((MainViewModel)ViewModel).ShowErrorMessage(e.ExceptionObject.ToString());
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_settings")
            };


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked ShowSettingsCommand"},

            });


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
            var coffeeRoomPickerViewModel = new MvxPickerViewModel(coffeeRoomPicker);
            coffeeRoomPicker.Model = coffeeRoomPickerViewModel;
            coffeeRoomPicker.ShowSelectionIndicator = true;
            CoffeeRoomNameTextField.InputView = coffeeRoomPicker;
            CoffeeRoomNameTextField.InputAccessoryView = toolbar;



            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(CurrentAmountLabel).To(vm => vm.CurrentBalance);
            set.Bind(CurrentShiftAmountLabel).To(vm => vm.CurrentShiftBalance);
            set.Bind(UpdateButton).To(vm => vm.UpdateEntireMoneyCommand);
            set.Bind(ShiftButton).To(vm => vm.ShowShiftsCommand);
            set.Bind(SupliedProductsButton).To(vm => vm.ShowSupliedProductsCommand);
            set.Bind(OrdersButton).To(vm => vm.ShowOrdersCommand);
            set.Bind(ProductsButton).To(vm => vm.EditProductsCommand);
            set.Bind(UsersButton).To(vm => vm.EditUsersCommand);
            set.Bind(StatisticButton).To(vm => vm.ShowStatiscticCommand);
            set.Bind(ExpensesButton).To(vm => vm.ShowExpensesCommand);
            set.Bind(InventoryButton).To(vm => vm.ShowInventoryCommand);
            set.Bind(UtilizeButton).To(vm => vm.ShowUtilizedSuplyProductsCommand);

            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);
            set.Bind(CoffeeRoomNameTextField).To(vm => vm.CurrentCoffeeRoomName);

            set.Apply();
            // Perform any additional setup after loading the view, typically from a nib.
        }


        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
        }

    }
}


using System;

using UIKit;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using CoreGraphics;
using MvvmCross.Binding.iOS.Views;

namespace CoffeeManagerAdmin.iOS
{
    public partial class MoneyView : ViewControllerBase<MoneyViewModel>
    {
        private MvxPickerViewModel coffeeRoomPickerViewModel;

        protected override bool UseCustomBackButton => false;

        public MoneyView() : base("MoneyView", null)
        {
        }

        protected override void DoViewDidLoad()
        {
            base.DoViewDidLoad();

            var settingsButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_settings")
            };

            var usersButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_account_circle")
            };

            this.AddBindings(new Dictionary<object, string>
            {
                {settingsButton, "Clicked ShowSettingsCommand"},
                {usersButton, "Clicked ShowUsersCommand"},

            });

            NavigationItem.SetRightBarButtonItems(new [] { settingsButton, usersButton}, false);

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
            var set = this.CreateBindingSet<MoneyView, MoneyViewModel>();
            set.Bind(CurrentShiftMoneyLabel).To(vm => vm.CurrentShiftBalance);
            set.Bind(EntireMoneyLabel).To(vm => vm.CurrentBalance);
            set.Bind(CreditCardAmountLabel).To(vm => vm.CurrentCreditCardBalance);
            set.Bind(RefreshMoneyButton).To(vm => vm.UpdateEntireMoneyCommand);
            set.Bind(ShiftsButton).To(vm => vm.ShowShiftsCommand);
            set.Bind(ProductsButton).To(vm => vm.ShowProductsCommand);
            set.Bind(CreditCardButton).To(vm => vm.ShowCreditCardCommand);

            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);
            set.Bind(CoffeeRoomTextField).To(vm => vm.CurrentCoffeeRoomName);
            set.Apply();
        }
    }
}


using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoreGraphics;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Home
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Финансы",
                        TabIconName = "ic_attach_money.png",
                        TabSelectedIconName = "ic_attach_money.png")]
    public partial class MoneyView : ViewControllerBase<MoneyViewModel>
    {
        private MvxPickerViewModel coffeeRoomPickerViewModel;
        private PagingTableSource shiftsSource;

        protected override bool UseCustomBackButton => false;

        public MoneyView() : base("MoneyView", null)
        {
        }

        protected override void DoViewDidLoad()
        {
            base.DoViewDidLoad();

            Title = "Финансы";
            
            var settingsButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_settings")
            };

            var usersButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_account_circle")
            };

            var creditCardButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_credit_card")
            };

            this.AddBindings(new Dictionary<object, string>
            {
                {settingsButton, "Clicked ShowSettingsCommand"},
                {usersButton, "Clicked ShowUsersCommand"}, 
                {creditCardButton, "Clicked ShowCreditCardCommand"},
            });

            NavigationItem.SetRightBarButtonItems(new [] { creditCardButton, usersButton}, true);

            NavigationItem.SetLeftBarButtonItem(settingsButton, false);

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

        protected override void InitStylesAndContent()
        {
            shiftsSource = new PagingTableSource(ShiftsTableView, ShiftInfoCell.Key, ShiftInfoCell.Nib);
            ShiftsTableView.Source = shiftsSource;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<MoneyView, MoneyViewModel>();
            set.Bind(CurrentShiftMoneyLabel).To(vm => vm.CurrentShiftBalance);
            set.Bind(EntireMoneyLabel).To(vm => vm.CurrentBalance);
            set.Bind(CreditCardAmountLabel).To(vm => vm.CurrentCreditCardBalance);
            set.Bind(RefreshMoneyButton).To(vm => vm.UpdateEntireMoneyCommand);

            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);
            set.Bind(CoffeeRoomTextField).To(vm => vm.CurrentCoffeeRoomName);

            set.Bind(shiftsSource).To(vm => vm.ItemsCollection);
            set.Bind(shiftsSource).For(s => s.GetNewPageCommand).To(vm => vm.LoadNextPageCommand).OneWay();
            set.Apply();
        }
    }
}


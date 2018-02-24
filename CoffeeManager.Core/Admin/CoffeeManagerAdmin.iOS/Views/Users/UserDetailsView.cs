using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using CoreGraphics;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserDetailsView : ViewControllerBase<UserDetailsViewModel>
    {
        public UserDetailsView() : base("UserDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Детали пользователя";
            var btn = new UIBarButtonItem();
            btn.Title = "Сохранить";

            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked UpdateCommand"},

            });

            var expenseTypePicker = new UIPickerView();
            var expensePickerViewModel = new MvxPickerViewModel(expenseTypePicker);
            expenseTypePicker.Model = expensePickerViewModel;
            expenseTypePicker.ShowSelectionIndicator = true;
            ExpenseTypeTextField.InputView = expenseTypePicker;

            var toolbar = new UIToolbar(new CGRect(0,0, this.View.Frame.Width, 44));
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
            toolbar.SetItems(new [] { doneButton}, false);
            ExpenseTypeTextField.InputAccessoryView = toolbar;

            var coffeeRoomPicker = new UIPickerView();
            var coffeeRoomPickerViewModel = new MvxPickerViewModel(coffeeRoomPicker);
            coffeeRoomPicker.Model = coffeeRoomPickerViewModel;
            coffeeRoomPicker.ShowSelectionIndicator = true;
            CoffeeRoomTextField.InputView = coffeeRoomPicker;
            CoffeeRoomTextField.InputAccessoryView = toolbar;


            var source = new SimpleTableSource(PenaltyTableView, UserPenaltyItemCell.Key, UserPenaltyItemCell.Nib, UserPenaltyTableHeaderView.Key, UserPenaltyTableHeaderView.Nib);
            PenaltyTableView.Source = source;

            var set = this.CreateBindingSet<UserDetailsView, UserDetailsViewModel>();
            set.Bind(NameTextField).To(vm => vm.UserName);
            set.Bind(NameTextField).For(e => e.Enabled).To(vm => vm.UserId).WithConversion(new GenericConverter<int, bool>((arg) => arg < 1));
            set.Bind(PaySalaryButton).To(vm => vm.PaySalaryCommand);
            set.Bind(UserEarningsButton).To(vm => vm.ShowEarningsCommand);
            set.Bind(PenaltyButton).To(vm => vm.PenaltyCommand);
            set.Bind(EntireSalaryLabel).To(vm => vm.EntireEarnedAmount).WithConversion(new DecimalToStringConverter());
            set.Bind(CurrentSalaryLabel).To(vm => vm.CurrentEarnedAmount).WithConversion(new DecimalToStringConverter());
            set.Bind(MinimimPaymentTextField).To(vm => vm.MinimumPayment);
            set.Bind(SalaryRateTextField).To(vm => vm.SalaryRate);
            set.Bind(DayPercentageTextField).To(vm => vm.DayShiftPersent);
            set.Bind(NightPercentageTextField).To(vm => vm.NightShiftPercent);
            set.Bind(ExpenseTypeTextField).To(vm => vm.ExpenseTypeName);
            set.Bind(expensePickerViewModel).For(p => p.ItemsSource).To(vm => vm.ExpenseItems);
            set.Bind(expensePickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedExpenseType);

            set.Bind(CoffeeRoomTextField).To(vm => vm.CurrentCoffeeRoomName);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);

            set.Bind(source).To(vm => vm.Penalties);
            set.Apply();
        }
    }
}


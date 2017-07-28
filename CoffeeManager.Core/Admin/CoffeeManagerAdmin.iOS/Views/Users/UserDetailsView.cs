using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using CoreGraphics;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserDetailsView : MvxViewController
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
    
            var set = this.CreateBindingSet<UserDetailsView, UserDetailsViewModel>();
            set.Bind(NameTextField).To(vm => vm.UserName);
            set.Bind(NameTextField).For(e => e.Enabled).To(vm => vm.UserId).WithConversion(new GenericConverter<int, bool>((arg) => arg < 1));
            set.Bind(PaySalaryButton).To(vm => vm.PaySalaryCommand);
            set.Bind(EntireSalaryLabel).To(vm => vm.EntireEarnedAmount).WithConversion(new DecimalToStringConverter());
            set.Bind(CurrentSalaryLabel).To(vm => vm.CurrentEarnedAmount).WithConversion(new DecimalToStringConverter());
            set.Bind(DayPercentageTextField).To(vm => vm.DayShiftPersent);
            set.Bind(NightPercentageTextField).To(vm => vm.NightShiftPercent);
            set.Bind(ExpenseTypeTextField).To(vm => vm.ExpenseTypeName);
            set.Bind(expensePickerViewModel).For(p => p.ItemsSource).To(vm => vm.ExpenseItems);
            set.Bind(expensePickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedExpenseType);
            set.Apply();
        }
    }
}


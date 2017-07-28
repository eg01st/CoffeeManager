using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.iOS.Views;
using CoreGraphics;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platform.iOS;

namespace CoffeeManagerAdmin.iOS
{
    public partial class StatisticView : MvxViewController
    {
        public StatisticView() : base("StatisticView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Статистика";

            var fromPicker = new UIDatePicker();
            fromPicker.Mode = UIDatePickerMode.Date;
            fromPicker.MinimumDate = new DateTime(2016, 5, 1).ToNSDate();
            fromPicker.MaximumDate = DateTime.Now.ToNSDate();
            FromTextField.InputView = fromPicker;

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
            FromTextField.InputAccessoryView = toolbar;

            var toPicker = new UIDatePicker();
            toPicker.MinimumDate = new DateTime(2016, 5, 1).ToNSDate();
            toPicker.MaximumDate = DateTime.Now.ToNSDate();
            toPicker.Mode = UIDatePickerMode.Date;
            ToTextField.InputView = toPicker;
            ToTextField.InputAccessoryView = toolbar;

            var set = this.CreateBindingSet<StatisticView, StatisticViewModel>();
            set.Bind(DoneButton).To(vm => vm.GetDataCommand);
            set.Bind(fromPicker).For(p => p.Date).To(vm => vm.From);
            set.Bind(toPicker).For(p => p.Date).To(vm => vm.To);
            set.Bind(FromTextField).To(vm => vm.From).WithConversion(new DateToStringConverter());
            set.Bind(ToTextField).To(vm => vm.To).WithConversion(new DateToStringConverter());;

            set.Apply();
        }
    }
}


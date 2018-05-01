using CoreGraphics;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Extensions
{
    public static class Helper
    {
        public static UIToolbar ProducePickerToolbar(UIView view, string title = "Готово")
        {
            var toolbar = new UIToolbar(new CGRect(0, 0, view.Frame.Width, 44));
            toolbar.UserInteractionEnabled = true;
            toolbar.BarStyle = UIBarStyle.BlackOpaque;
            var doneButton = new UIBarButtonItem();
            doneButton.Title = title;
            doneButton.Style = UIBarButtonItemStyle.Bordered;
            doneButton.TintColor = UIColor.Black;
            doneButton.Clicked += (sender, e) =>
            {
                view.EndEditing(true);
            };
            toolbar.SetItems(new[] { doneButton }, false);
            return toolbar;
        }

        public static MvxPickerViewModel ProducePicker(UITextField pickerTextField, UIToolbar toolbar)
        {
            var picker = new UIPickerView();
            var viewModel = new MvxPickerViewModel(picker);
            picker.Model = viewModel;
            picker.ShowSelectionIndicator = true;
            pickerTextField.InputView = picker;
            pickerTextField.InputAccessoryView = toolbar;

            return viewModel;
        }
    }
}
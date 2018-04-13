using System;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoreGraphics;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class TransferSuplyProductsView : ViewControllerBase<TransferSuplyProductsViewModel>
    {
        private MvxPickerViewModel coffeeRoomFromPickerViewModel;
        private MvxPickerViewModel coffeeRoomToPickerViewModel;

        public TransferSuplyProductsView() : base("TransferSuplyProductsView", null)
        {
        }

        protected override void DoViewDidLoad()
        {
            base.DoViewDidLoad();

            Title = "Перемешение продуктов по складам";

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


            var coffeeFromRoomPicker = new UIPickerView();
            coffeeRoomFromPickerViewModel = new MvxPickerViewModel(coffeeFromRoomPicker);
            coffeeFromRoomPicker.Model = coffeeRoomFromPickerViewModel;
            coffeeFromRoomPicker.ShowSelectionIndicator = true;
            CoffeeRoomFromTextField.InputView = coffeeFromRoomPicker;
            CoffeeRoomFromTextField.InputAccessoryView = toolbar;

            var coffeeRoomToPicker = new UIPickerView();
            coffeeRoomToPickerViewModel = new MvxPickerViewModel(coffeeRoomToPicker);
            coffeeRoomToPicker.Model = coffeeRoomToPickerViewModel;
            coffeeRoomToPicker.ShowSelectionIndicator = true;
            CoffeeRoomToTextField.InputView = coffeeRoomToPicker;
            CoffeeRoomToTextField.InputAccessoryView = toolbar;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<TransferSuplyProductsView, TransferSuplyProductsViewModel>();
           
            set.Bind(coffeeRoomFromPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomFromPickerViewModel).For(p => p.SelectedItem).To(vm => vm.FromCoffeeRoom);
            set.Bind(CoffeeRoomFromTextField).To(vm => vm.FromCoffeeRoomName);


            set.Bind(coffeeRoomToPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomToPickerViewModel).For(p => p.SelectedItem).To(vm => vm.ToCoffeeRoom);
            set.Bind(CoffeeRoomToTextField).To(vm => vm.ToCoffeeRoomName);

            set.Bind(TransferButton).To(vm => vm.NextCommand);

            set.Apply();
        }

    }
}


using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using CoreGraphics;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    public partial class CoffeeCountersView : ViewControllerBase<CoffeeCountersViewModel>
    {
        private SimpleTableSource tableSource;

        private MvxPickerViewModel coffeeRoomPickerViewModel;

        public CoffeeCountersView() : base("CoffeeCountersView", null)
        {
        }
        
        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            var addCounterButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(addCounterButton, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {addCounterButton, "Clicked AddCounterCommand"},
            });
        }

        protected override void InitStylesAndContent()
        {
            Title = "Счетчики кофемолок";
            tableSource = new SimpleTableSource(CountersTableView, CounterTableViewCell.Key, CounterTableViewCell.Nib);
            CountersTableView.Source = tableSource;

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
            base.DoBind();
            var set = this.CreateBindingSet<CoffeeCountersView, CoffeeCountersViewModel>();
            set.Bind(tableSource).To(vm => vm.ItemsCollection);
            set.Bind(tableSource).For(d => d.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Bind(CoffeeRoomTextField).To(vm => vm.CurrentCoffeeRoomName);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.ItemsSource).To(vm => vm.CoffeeRooms);
            set.Bind(coffeeRoomPickerViewModel).For(p => p.SelectedItem).To(vm => vm.CurrentCoffeeRoom);
            set.Apply();
        }
    }
}


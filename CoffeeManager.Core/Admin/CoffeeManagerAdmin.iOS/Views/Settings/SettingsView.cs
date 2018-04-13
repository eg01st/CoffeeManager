using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Settings
{
    public partial class SettingsView : ViewControllerBase<SettingsViewModel>
    {
        private SimpleTableSource clientTableSource;
        private SimpleTableSource coffeeRoomsTableSource;


        public bool IsAdmin
        {
            set
            {
                if (value)
                {
                    var btn = new UIBarButtonItem()
                    {
                        Image = UIImage.FromBundle("ic_add_circle_outline")
                    };


                    NavigationItem.SetRightBarButtonItem(btn, false);
                    this.AddBindings(new Dictionary<object, string>
                    {
                        {btn, "Clicked AddUserCommand"},

                    });
                }
            }
        }

        public SettingsView() : base("SettingsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Настройки";
            // Perform any additional setup after loading the view, typically from a nib.
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            clientTableSource = new SimpleTableSource(ClientsTableView, ClientViewCell.Key, ClientViewCell.Nib);
            ClientsTableView.Source = clientTableSource;

            coffeeRoomsTableSource = new SimpleTableSource(CoffeeRoomsTable, CoffeeRoomItemViewCell.Key, CoffeeRoomItemViewCell.Nib);
            CoffeeRoomsTable.Source = coffeeRoomsTableSource;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<SettingsView, SettingsViewModel>();
            set.Bind(CoffeeRoonNameTextField).To(vm => vm.NewCoffeeroomName);
            set.Bind(AddCoffeeRoomButton).To(vm => vm.AddCoffeeRoomCommand);
            set.Bind(clientTableSource).To(vm => vm.Clients);
            set.Bind(coffeeRoomsTableSource).To(vm => vm.CoffeeRooms);
            set.Bind(ClientsTableView).For(t => t.Hidden).To(vm => vm.IsAdmin).WithConversion(new InvertedBoolConverter());
            set.Bind(this).For(nameof(IsAdmin)).To(vm => vm.IsAdmin);
            set.Bind(CountersButton).To(vm => vm.ShowCountersCommand);
            set.Apply();
        }
    }
}


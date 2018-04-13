using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Users
{
    public partial class UsersView : ViewControllerBase<UsersViewModel>
    {
        public UsersView() : base("UsersView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Персонал";

            var btn = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddUserCommand"},
            });

            var source = new SimpleTableSource(UsersTableView, UserTableViewCell.Key, UserTableViewCell.Nib);
            UsersTableView.Source = source;
        
            var set = this.CreateBindingSet<UsersView, UsersViewModel>();
            set.Bind(source).To(vm => vm.Users);
            set.Bind(AmountForSalaryPayLabel).To(vm => vm.AmountToPay);
            set.Apply();
            
      
        }

    }
}


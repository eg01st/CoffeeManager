using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;
using System.Collections.Generic;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UsersView : MvxViewController
    {
        private UITapGestureRecognizer addUserRegognizer;

        private ICommand _addUserCommand;
        public ICommand AddUserCommand
        {
            get { return _addUserCommand; }
            set
            {
                _addUserCommand = value;

            }
        }

        public UsersView() : base("UsersView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Персонал";
             var btn = new UIBarButtonItem();
            btn.Title = "Добавить";

            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddUserCommand"},
            });

            var source = new SimpleTableSource(UsersTableView, UserTableViewCell.Key, UserTableViewCell.Nib);
            UsersTableView.Source = source;
        
            var set = this.CreateBindingSet<UsersView, UsersViewModel>();
            set.Bind(source).To(vm => vm.Users);
            set.Apply();
            
      
        }

    }
}


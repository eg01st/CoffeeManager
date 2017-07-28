﻿using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UserTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("UserTableViewCell");
        public static readonly UINib Nib;

        static UserTableViewCell()
        {
            Nib = UINib.FromName("UserTableViewCell", NSBundle.MainBundle);
        }

        protected UserTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        private ICommand _toggleIsActiveCommand;
        public ICommand ToggleIsActiveCommand
        {
            get { return _toggleIsActiveCommand; }
            set
            {
                _toggleIsActiveCommand = value;
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<UserTableViewCell, UserItemViewModel>();
                set.Bind(NameLabel).To(vm => vm.UserName);
                set.Bind(IsActiveSwitch).For(s => s.On).To(vm => vm.IsActive);
                set.Bind(this).For(t => t.ToggleIsActiveCommand).To(vm => vm.ToggleIsActiveCommand);
                set.Apply();    
                IsActiveSwitch.ValueChanged += (sender, e) => 
                {
                    ToggleIsActiveCommand.Execute(null);
                };    
            });
        }
    }
}

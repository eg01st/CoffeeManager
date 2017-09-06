﻿using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddExpenseTypeView : MvxViewController<AddExpenseTypeViewModel>
    {
        public AddExpenseTypeView() : base("AddExpenseTypeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<AddExpenseTypeView, AddExpenseTypeViewModel>();
            set.Bind(NameTextView).To(vm => vm.ExpenseName);
            set.Bind(AddButton).To(vm => vm.AddExpenseTypeCommand);
            set.Apply();
        }

   
    }
}

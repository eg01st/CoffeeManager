using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddExpenseTypeView : ViewControllerBase<AddExpenseTypeViewModel>
    {
        public AddExpenseTypeView() : base("AddExpenseTypeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Добавить расход";
            var set = this.CreateBindingSet<AddExpenseTypeView, AddExpenseTypeViewModel>();
            set.Bind(NameTextView).To(vm => vm.ExpenseName);
            set.Bind(AddButton).To(vm => vm.AddExpenseTypeCommand);
            set.Apply();
        }

   
    }
}


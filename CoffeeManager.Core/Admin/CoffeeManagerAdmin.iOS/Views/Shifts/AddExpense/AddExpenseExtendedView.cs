using System;
using CoffeeManagerAdmin.Core;
using UIKit;
using MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddExpenseExtendedView : ViewControllerBase<AddExpenseExtendedViewModel>
    {
        private SimpleTableSource source;

        public AddExpenseExtendedView() : base("AddExpenseExtendedView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            source = new SimpleTableSource(TableView, AddExpenseExtendedTableCell.Key, AddExpenseExtendedTableCell.Nib,
                                           AddExpenseExtendedTableHeader.Key, AddExpenseExtendedTableHeader.Nib);
            TableView.Source = source;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<AddExpenseExtendedView, AddExpenseExtendedViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var btn = new UIBarButtonItem()
            {
                Title = "Готово"
            };

            StickBottomButtonToKeyboard(BottomConstraint);


            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked SaveExpenseCommand"},

            });
        }



    }
}


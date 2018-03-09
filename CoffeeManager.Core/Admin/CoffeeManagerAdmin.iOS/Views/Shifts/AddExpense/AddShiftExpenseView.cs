﻿using System;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddShiftExpenseView : SearchViewController<AddShiftExpenseView, AddShiftExpenseViewModel, AddExpenseItemViewModel>
    {
        public AddShiftExpenseView() : base("AddShiftExpenseView", null)
        {
        }

        protected override SimpleTableSource TableSource => new SimpleTableSource(TableView, SimpleExpenseCell.Key, SimpleExpenseCell.Nib);
        protected override UIView TableViewContainer => this.View;


        protected override MvxFluentBindingDescriptionSet<AddShiftExpenseView, AddShiftExpenseViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<AddShiftExpenseView, AddShiftExpenseViewModel>();
        }

        protected override void DoViewDidLoad()
        {
            base.DoViewDidLoad();
            Title = "Добавить расход";
        }
    }
}

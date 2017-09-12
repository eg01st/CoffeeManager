﻿using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public partial class UtilizeView : ViewControllerBase
    {
        public UtilizeView() : base("UtilizeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Списания";
            var source = new SimpleTableSourceWithHeader(UtilizedTableView, UtilizeItemCell.Key, UtilizeItemCell.Nib, UtilizeItemHeader.Key);
            UtilizedTableView.Source = source;
            UtilizedTableView.RegisterNibForHeaderFooterViewReuse(UtilizeItemHeader.Nib, UtilizeItemHeader.Key);
            var set = this.CreateBindingSet<UtilizeView, UtilizeViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }

    }
}


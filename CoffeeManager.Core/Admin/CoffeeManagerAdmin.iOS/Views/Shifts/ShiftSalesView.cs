using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftSalesView : ViewControllerBase<ShiftSalesViewModel> 
    {
        public ShiftSalesView() : base("ShiftSalesView", null)
        {
        }

        private SaleTableSource tableSource;

        List<SaleItemViewModel> _saleItems;

        public List<SaleItemViewModel> SaleItems
        {
            get { return _saleItems; }
            set
            {
                _saleItems = value;
                if (tableSource.ItemsSource.Count() < 1)
                {
                    tableSource.ItemsSource = _saleItems;
                    SalesTableView.ReloadData();
                }
            }
        }


        List<Entity> _groupedSaleItems;

        public List<Entity> GroupedSaleItems
        {
            get { return _groupedSaleItems; }
            set
            {
                _groupedSaleItems = value;
                if (tableSource.ItemsSource.Count() < 1)
                {
                    tableSource.ItemsSource = _groupedSaleItems;
                    SalesTableView.ReloadData();
                }
            }
        }



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Продажи за смену";

            SegmentControl.ValueChanged += SegmentControl_ValueChanged;

            tableSource = new SaleTableSource(SalesTableView);
            SalesTableView.Source = tableSource;

            var set = this.CreateBindingSet<ShiftSalesView, ShiftSalesViewModel>();
            set.Bind(this).For(i => i.SaleItems).To(vm => vm.SaleItems);
            set.Bind(this).For(i => i.GroupedSaleItems).To(vm => vm.GroupedSaleItems);
            set.Apply();
        }


        void SegmentControl_ValueChanged(object sender, EventArgs e)
        {
            if(SegmentControl.SelectedSegment == 0)
            {
                tableSource.ItemsSource = _saleItems;

            }
            else if(SegmentControl.SelectedSegment == 1)
            {
                tableSource.ItemsSource = _groupedSaleItems;
            }
            SalesTableView.ReloadData();
        }
    }
}


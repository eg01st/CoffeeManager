using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftSalesView : MvxViewController 
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
            var g = new UITapGestureRecognizer((obj) =>
               {
                   tableSource.ItemsSource = _saleItems;
                   SalesTableView.ReloadData();
               });
            ByTimeButton.AddGestureRecognizer(g);

            var g1 = new UITapGestureRecognizer((obj) =>
            {
                tableSource.ItemsSource = _groupedSaleItems;
                SalesTableView.ReloadData();
            });
            ByProductButton.AddGestureRecognizer(g1);

            tableSource = new SaleTableSource(SalesTableView);
            SalesTableView.Source = tableSource;


            var set = this.CreateBindingSet<ShiftSalesView, ShiftSalesViewModel>();
            set.Bind(this).For(i => i.SaleItems).To(vm => vm.SaleItems);
            set.Bind(this).For(i => i.GroupedSaleItems).To(vm => vm.GroupedSaleItems);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


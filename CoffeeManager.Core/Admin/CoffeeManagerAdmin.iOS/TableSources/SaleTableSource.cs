using System;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using CoffeeManagerAdmin.Core;
using Foundation;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.iOS
{
    public class SaleTableSource : MvxTableViewSource
    {
        private static readonly NSString SaleItemCellIdentifier = new NSString("SaleItemCell");
        private static readonly NSString GroupedSaleCellIdentifier = new NSString("GroupedSaleCell");

        public SaleTableSource(UITableView tableView) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(UINib.FromName("SaleItemCell", NSBundle.MainBundle),
                                            SaleItemCellIdentifier);
            tableView.RegisterNibForCellReuse(UINib.FromName("GroupedSaleCell", NSBundle.MainBundle), GroupedSaleCellIdentifier);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, Foundation.NSIndexPath indexPath, object item)
        {

            NSString cellIdentifier;
            if (item is SaleItemViewModel)
            {
                cellIdentifier = SaleItemCellIdentifier;
            }
            else if (item is Entity)
            {
                cellIdentifier = GroupedSaleCellIdentifier;
            }
            else
            {
                throw new ArgumentException("Unknown type " + item.GetType().Name);
            }

            return (UITableViewCell)TableView.DequeueReusableCell(cellIdentifier, indexPath);

        }
    }
}

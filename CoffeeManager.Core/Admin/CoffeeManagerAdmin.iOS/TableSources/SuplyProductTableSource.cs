using System;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using CoffeManager.Common;
using Foundation;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public class SuplyProductTableSource : SimpleTableSource
    {
        protected readonly NSString headerKey;
        public SuplyProductTableSource(UITableView tableView, NSString key, UINib nib, NSString headerKey, UINib headerNib) : base(tableView, key, nib)
        {
            this.headerKey = headerKey;
            tableView.RegisterNibForCellReuse(headerNib, headerKey);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            if (item is SuplyProductItemViewModel)
            {
                return tableView.DequeueReusableCell(reuseIdentifier, indexPath);
            }
            else
            {
                return tableView.DequeueReusableCell(headerKey, indexPath);
            }
        }
    }
}

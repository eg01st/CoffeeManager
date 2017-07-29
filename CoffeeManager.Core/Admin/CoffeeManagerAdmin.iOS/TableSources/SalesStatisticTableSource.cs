using System;
using UIKit;
using Foundation;
namespace CoffeeManagerAdmin.iOS
{
    public class SalesStatisticTableSource : SimpleTableSource
    {
        public SalesStatisticTableSource(UITableView tableView, NSString key, UINib nib) : base(tableView, key, nib)
        {
        }
    }
}

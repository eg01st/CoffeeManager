using System;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class SuplyProductTableSource : MvxSimpleTableViewSource
    {
        public SuplyProductTableSource(UITableView tableView) : base(tableView, SuplyProductCell.Key)
        {

        }
    }
}

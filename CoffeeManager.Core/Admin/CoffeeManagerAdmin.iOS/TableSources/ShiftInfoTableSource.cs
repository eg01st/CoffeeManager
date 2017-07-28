using System;
using MvvmCross.Binding.iOS.Views;
using UIKit;
namespace CoffeeManagerAdmin.iOS
{
    public class ShiftInfoTableSource : MvxSimpleTableViewSource
    {
        public ShiftInfoTableSource(UITableView tableView) : base(tableView, ShiftInfoCell.Key)
        {

        }
    }
}

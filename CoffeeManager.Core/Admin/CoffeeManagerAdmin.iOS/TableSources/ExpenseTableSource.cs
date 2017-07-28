using System;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class ExpenseTableSource : MvxSimpleTableViewSource
    {
        public ExpenseTableSource(UITableView tableView) : base(tableView, ExpenseItemCell.Key)
        {

        }
    }
}

using System;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.iOS.Views.Home.SideMenu;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class MenuTableSource: MvxTableViewSource
    {
        public MenuTableSource(UITableView tableView)
                    : base(tableView)
        {
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            if (item is MenuFeedtemViewModel)
            {
                return tableView.DequeueReusableCell(MenuCell.Key, indexPath) as MenuCell;
            }
            else
            {
                return tableView.DequeueReusableCell(MenuHeaderCell.Key, indexPath);
            }
        }
    }
}

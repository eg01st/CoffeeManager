using System;
using MvvmCross.Binding.iOS.Views;
using CoffeeManagerAdmin.Core;
using Foundation;
using System.Collections.Generic;
using UIKit;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.iOS
{
    public class BaseTableSource<T> : MvxTableViewSource where T: ListItemViewModelBase
    {
        protected readonly NSString reuseIdentifier;
        private List<ListItemViewModelBase> Source => ItemsSource as List<ListItemViewModelBase>;

        public BaseTableSource(UITableView tableView, NSString reuseIdentifier, UINib cellNib) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(cellNib, reuseIdentifier);
            this.reuseIdentifier = reuseIdentifier;
        }
        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var item = GetItemAt(indexPath);
            var vm = item as ListItemViewModelBase;
            if(vm != null)
            {
                vm.GoToDetailsCommand.Execute(null);
            }
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(reuseIdentifier, indexPath);
        }
    }
}

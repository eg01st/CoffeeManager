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
        protected readonly NSString headerReuseIdentifier;

        private List<ListItemViewModelBase> Source => ItemsSource as List<ListItemViewModelBase>;

        public BaseTableSource(UITableView tableView, NSString reuseIdentifier, UINib cellNib) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(cellNib, reuseIdentifier);
            this.reuseIdentifier = reuseIdentifier;
            tableView.RowHeight = UITableView.AutomaticDimension;
            tableView.EstimatedRowHeight = 100f;
        }

        public BaseTableSource(UITableView tableView, NSString reuseIdentifier,
                               UINib cellNib, NSString headerReuseIdentifier, UINib headerlNib) : this(tableView, reuseIdentifier, cellNib)
        {
            this.headerReuseIdentifier = headerReuseIdentifier;
            tableView.RegisterNibForHeaderFooterViewReuse(headerlNib, headerReuseIdentifier);
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var item = GetItemAt(indexPath);
            var vm = item as ListItemViewModelBase;
            if(vm != null)
            {
                vm.GoToDetailsCommand.Execute(null);
                tableView.DeselectRow(indexPath, true);
            }
        }


        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return headerReuseIdentifier == null ? 0 : 30;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            if(headerReuseIdentifier == null)
            {
                return base.GetViewForHeader(tableView, section);
            }
            return tableView.DequeueReusableHeaderFooterView(headerReuseIdentifier);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(reuseIdentifier, indexPath);
        }
    }
}

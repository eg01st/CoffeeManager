using System;
using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class SimpleTableSourceWithHeader : SimpleTableSource
    {
        readonly NSString headerReuseIdentifier;

        public SimpleTableSourceWithHeader(UITableView tableView, NSString reuseIdentifier, UINib cellNib, NSString headerReuseIdentifier) : base(tableView, reuseIdentifier, cellNib)
        {
            this.headerReuseIdentifier = headerReuseIdentifier;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 30;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            return tableView.DequeueReusableHeaderFooterView(headerReuseIdentifier);
        }
    }
}

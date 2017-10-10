using CoffeeManagerAdmin.Core;
using CoffeManager.Common;
using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class SimpleTableSource : BaseTableSource<ListItemViewModelBase>
    {
        public SimpleTableSource(UITableView tableView, NSString reuseIdentifier, UINib cellNib) : base(tableView, reuseIdentifier, cellNib)
        {
        }

        public SimpleTableSource(UITableView tableView, NSString reuseIdentifier, UINib cellNib,
                                 NSString headerReuseIdentifier, UINib headerlNib) : base(tableView, reuseIdentifier, cellNib, headerReuseIdentifier, headerlNib)
        {
        }
    }
}
    
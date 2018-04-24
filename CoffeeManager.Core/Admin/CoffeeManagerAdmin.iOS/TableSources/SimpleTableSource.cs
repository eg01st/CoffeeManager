using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS.TableSources
{
    public class SimpleTableSource : BaseTableSource<MobileCore.ViewModels.FeedItemElementViewModel>
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
    
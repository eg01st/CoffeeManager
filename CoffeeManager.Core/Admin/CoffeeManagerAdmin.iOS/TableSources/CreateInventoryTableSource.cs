using UIKit;
using Foundation;
using CoffeeManagerAdmin.Core;

namespace CoffeeManagerAdmin.iOS
{
    public class CreateInventoryTableSource : SimpleTableSource
    {
        public CreateInventoryTableSource(UITableView tableView, NSString key, UINib nib) : base(tableView, key, nib)
        {

        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            //if(indexPath.Row == 0)
            //{
            //    return tableView.DequeueReusableCell(headerKey, indexPath);
            //}
            //else
            //{
                return tableView.DequeueReusableCell(reuseIdentifier, indexPath);
           // }

            //if (!(item is ExpenseTypeHeaderViewModel))
            //{
                
            //}
            //else
            //{
            //    return tableView.DequeueReusableCell(headerKey, indexPath);
            //}
        }
    }
}

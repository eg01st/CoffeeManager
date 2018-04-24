using System;
using UIKit;
using Foundation;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.TableSources;

namespace CoffeeManagerAdmin.iOS
{
    public class SalesStatisticTableSource : BaseTableSource<BaseStatisticSaleItemViewModel>
    {
        protected readonly NSString headerKey;
        public SalesStatisticTableSource(UITableView tableView, NSString key, UINib nib, NSString headerKey, UINib headerNib) : base(tableView, key, nib)
        {
            this.headerKey = headerKey;
            tableView.RegisterNibForCellReuse(headerNib, headerKey);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            if(item is StatisticSaleItemViewModel)
            {
                return tableView.DequeueReusableCell(reuseIdentifier, indexPath);            
            }
            else
            {
                return tableView.DequeueReusableCell(headerKey, indexPath);     
            }  
        }
    }
}

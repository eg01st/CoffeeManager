using System;
using System.Linq;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using Foundation;
using MvvmCross.Binding.ExtensionMethods;
using UIKit;

namespace CoffeeManagerAdmin.iOS
{
    public class PagingTableSource : BaseTableSource<ListItemViewModelBase>
    {
        protected static readonly nfloat PercentsScrolledThreshold = 75.0f;

        protected ICommand getNewPageCommand;
        protected int currentPage = -1;

        private bool nextPageRequested = false;


        public PagingTableSource(UITableView tableView, NSString reuseIdentifier, UINib cellNib) : base(tableView, reuseIdentifier, cellNib)
        {
        }

        public PagingTableSource(UITableView tableView, NSString reuseIdentifier,
                                     UINib cellNib, NSString headerReuseIdentifier, UINib headerlNib) : base(tableView, reuseIdentifier, cellNib, headerReuseIdentifier, headerlNib)
        {

        }

        public ICommand GetNewPageCommand
        {
            get { return getNewPageCommand; }
            set { getNewPageCommand = value; }
        }

        protected override void CollectionChangedOnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
        {
            base.CollectionChangedOnCollectionChanged(sender, args);

            nextPageRequested = false;
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            if (getNewPageCommand == null)
            {
                return;
            }

            if (nextPageRequested == true)
            {
                return;
            }

            var paths = TableView.IndexPathsForVisibleRows;
            if (paths.Length <= 0)
            {
                return;
            }

            var maxRow = paths.Max(p => p.LongRow);
            var totalRows = ItemsSource.Count();
            var currentPercents = (maxRow * 100.0f) / totalRows;

            if (currentPercents < PercentsScrolledThreshold)
            {
                return;
            }

            if (getNewPageCommand.CanExecute(null) == false)
            {
                return;
            }

            nextPageRequested = true;
            getNewPageCommand.Execute(null);
        }
    }
}

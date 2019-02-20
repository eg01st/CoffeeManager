﻿using System;
using System.Collections.Generic;
using CoffeManager.Common.ViewModels;
using Foundation;
using MobileCore.ViewModels;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using System.Windows.Input;
using System.Linq;

namespace CoffeeManagerAdmin.iOS.TableSources
{
    public class BaseTableSource<T> : MvxTableViewSource where T: FeedItemElementViewModel
    {
        private const string longPressGestureRecognizerName = "longPressGestureRecognizerName";

        protected readonly NSString reuseIdentifier;
        protected readonly NSString headerReuseIdentifier;

        private UILongPressGestureRecognizer longPressGestureRecognizer;

        private ICommand longPressCommand;

        public ICommand LongPressCommand
        {
            get => longPressCommand;
            set
            {
                longPressCommand = value;
                if (longPressGestureRecognizer == null)
                {
                    longPressGestureRecognizer = new UILongPressGestureRecognizer((obj) =>
                    {
                        if (obj.State == UIGestureRecognizerState.Began)
                        {
                            var point = obj.LocationInView(this.TableView);
                            var index = TableView.IndexPathForRowAtPoint(point);
                            var item = GetItemAt(index) as T;
                            longPressCommand.Execute(item);
                        }
                    });
                    longPressGestureRecognizer.Name = longPressGestureRecognizerName;
                    longPressGestureRecognizer.MinimumPressDuration = 1;
                }
                if(TableView.GestureRecognizers.All(g => !g.Name?.Equals(longPressGestureRecognizerName) ?? true))
                {
                    TableView.AddGestureRecognizer(longPressGestureRecognizer);
                }

            }
        }

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

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            SelectionChangedCommand?.Execute(GetItemAt(indexPath));
            tableView.DeselectRow(indexPath, true);
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

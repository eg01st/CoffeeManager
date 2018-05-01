﻿using System;
using CoffeeManagerAdmin.iOS.Extensions;
using Foundation;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Controls
{
    public class ColoredPickerView : UIPickerView
    {
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var value = Model.GetTitle(this, indexPath.Row, 0);

            var cell = base.GetCell(tableView, indexPath);
            var view = new UIView() { BackgroundColor = ColorExtensions.ParseColorFromHex(value) };
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            cell.AddSubview(view);

            cell.AddConstraints(ConstraintExtensions.StickToAllSuperViewEdges(cell, view));

            return cell;
        }
    }
}
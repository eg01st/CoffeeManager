// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
	[Register ("ShiftExpenseTableViewCell")]
	partial class ShiftExpenseTableViewCell
	{
		[Outlet]
		UIKit.UILabel ExpenseQuantitnyName { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		UIKit.UILabel QuantityLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (QuantityLabel != null) {
				QuantityLabel.Dispose ();
				QuantityLabel = null;
			}

			if (ExpenseQuantitnyName != null) {
				ExpenseQuantitnyName.Dispose ();
				ExpenseQuantitnyName = null;
			}
		}
	}
}

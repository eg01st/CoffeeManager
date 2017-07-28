// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS
{
	[Register ("ShiftInfoCell")]
	partial class ShiftInfoCell
	{
		[Outlet]
		UIKit.UILabel CreditCardAmount { get; set; }

		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UILabel ExpenseLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel RealAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel ShiftEarnedAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel StartAmount { get; set; }

		[Outlet]
		UIKit.UILabel TotalLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (ExpenseLabel != null) {
				ExpenseLabel.Dispose ();
				ExpenseLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (RealAmountLabel != null) {
				RealAmountLabel.Dispose ();
				RealAmountLabel = null;
			}

			if (ShiftEarnedAmountLabel != null) {
				ShiftEarnedAmountLabel.Dispose ();
				ShiftEarnedAmountLabel = null;
			}

			if (StartAmount != null) {
				StartAmount.Dispose ();
				StartAmount = null;
			}

			if (TotalLabel != null) {
				TotalLabel.Dispose ();
				TotalLabel = null;
			}

			if (CreditCardAmount != null) {
				CreditCardAmount.Dispose ();
				CreditCardAmount = null;
			}
		}
	}
}

// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Shifts.Counters
{
	[Register ("ShiftCounterTableViewCell")]
	partial class ShiftCounterTableViewCell
	{
		[Outlet]
		UIKit.UILabel DiffLabel { get; set; }

		[Outlet]
		UIKit.UILabel FinishLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel StartLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FinishLabel != null) {
				FinishLabel.Dispose ();
				FinishLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (StartLabel != null) {
				StartLabel.Dispose ();
				StartLabel = null;
			}

			if (DiffLabel != null) {
				DiffLabel.Dispose ();
				DiffLabel = null;
			}
		}
	}
}

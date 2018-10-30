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
	[Register ("AutoOrderItemViewCell")]
	partial class AutoOrderItemViewCell
	{
		[Outlet]
		UIKit.UILabel DayOfWeekLabel { get; set; }

		[Outlet]
		UIKit.UILabel HourLabel { get; set; }

		[Outlet]
		UIKit.UISwitch IsActiveSwitch { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DayOfWeekLabel != null) {
				DayOfWeekLabel.Dispose ();
				DayOfWeekLabel = null;
			}

			if (HourLabel != null) {
				HourLabel.Dispose ();
				HourLabel = null;
			}

			if (IsActiveSwitch != null) {
				IsActiveSwitch.Dispose ();
				IsActiveSwitch = null;
			}
		}
	}
}

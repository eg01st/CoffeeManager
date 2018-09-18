// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Motivation
{
	[Register ("MotivationViewCell")]
	partial class MotivationViewCell
	{
		[Outlet]
		UIKit.UILabel EntireScoreLabel { get; set; }

		[Outlet]
		UIKit.UILabel MoneyScoreLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel ShiftScoreLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (EntireScoreLabel != null) {
				EntireScoreLabel.Dispose ();
				EntireScoreLabel = null;
			}

			if (MoneyScoreLabel != null) {
				MoneyScoreLabel.Dispose ();
				MoneyScoreLabel = null;
			}

			if (ShiftScoreLabel != null) {
				ShiftScoreLabel.Dispose ();
				ShiftScoreLabel = null;
			}
		}
	}
}

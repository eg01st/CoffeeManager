// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Motivation
{
	[Register ("MotivationView")]
	partial class MotivationView
	{
		[Outlet]
		UIKit.UIButton FinishMotivationButton { get; set; }

		[Outlet]
		UIKit.UILabel MotivationStartDateLabel { get; set; }

		[Outlet]
		UIKit.UITableView MotivationTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MotivationStartDateLabel != null) {
				MotivationStartDateLabel.Dispose ();
				MotivationStartDateLabel = null;
			}

			if (FinishMotivationButton != null) {
				FinishMotivationButton.Dispose ();
				FinishMotivationButton = null;
			}

			if (MotivationTableView != null) {
				MotivationTableView.Dispose ();
				MotivationTableView = null;
			}
		}
	}
}

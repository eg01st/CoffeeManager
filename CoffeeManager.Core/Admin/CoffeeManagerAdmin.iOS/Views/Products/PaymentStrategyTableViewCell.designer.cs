// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
	[Register ("PaymentStrategyTableViewCell")]
	partial class PaymentStrategyTableViewCell
	{
		[Outlet]
		UIKit.UILabel CoffeeRoomNameLabel { get; set; }

		[Outlet]
		UIKit.UITextField DayPercentTextField { get; set; }

		[Outlet]
		UIKit.UITextField NightPercentTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NightPercentTextField != null) {
				NightPercentTextField.Dispose ();
				NightPercentTextField = null;
			}

			if (DayPercentTextField != null) {
				DayPercentTextField.Dispose ();
				DayPercentTextField = null;
			}

			if (CoffeeRoomNameLabel != null) {
				CoffeeRoomNameLabel.Dispose ();
				CoffeeRoomNameLabel = null;
			}
		}
	}
}

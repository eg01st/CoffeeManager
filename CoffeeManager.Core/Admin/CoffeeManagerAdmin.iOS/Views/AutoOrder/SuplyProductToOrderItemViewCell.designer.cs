// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.AutoOrder
{
	[Register ("SuplyProductToOrderItemViewCell")]
	partial class SuplyProductToOrderItemViewCell
	{
		[Outlet]
		UIKit.UITextField QuantityAfterTextField { get; set; }

		[Outlet]
		UIKit.UISwitch ShouldUpdateQuantityBeforeOrderSwitch { get; set; }

		[Outlet]
		UIKit.UILabel SuplyProductNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (QuantityAfterTextField != null) {
				QuantityAfterTextField.Dispose ();
				QuantityAfterTextField = null;
			}

			if (SuplyProductNameLabel != null) {
				SuplyProductNameLabel.Dispose ();
				SuplyProductNameLabel = null;
			}

			if (ShouldUpdateQuantityBeforeOrderSwitch != null) {
				ShouldUpdateQuantityBeforeOrderSwitch.Dispose ();
				ShouldUpdateQuantityBeforeOrderSwitch = null;
			}
		}
	}
}

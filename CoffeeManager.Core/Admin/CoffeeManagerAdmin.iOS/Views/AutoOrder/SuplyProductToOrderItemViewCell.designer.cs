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
	[Register ("SuplyProductToOrderItemViewCell")]
	partial class SuplyProductToOrderItemViewCell
	{
		[Outlet]
		UIKit.UITextField QuantityAfterTextField { get; set; }

		[Outlet]
		UIKit.UILabel SuplyProductNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SuplyProductNameLabel != null) {
				SuplyProductNameLabel.Dispose ();
				SuplyProductNameLabel = null;
			}

			if (QuantityAfterTextField != null) {
				QuantityAfterTextField.Dispose ();
				QuantityAfterTextField = null;
			}
		}
	}
}

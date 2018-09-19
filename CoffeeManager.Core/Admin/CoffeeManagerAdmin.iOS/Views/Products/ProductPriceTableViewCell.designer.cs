// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
	[Register ("ProductPriceTableViewCell")]
	partial class ProductPriceTableViewCell
	{
		[Outlet]
		UIKit.UILabel CoffeeRoomNameLabel { get; set; }

		[Outlet]
		UIKit.UITextField DiscountPriceTextField { get; set; }

		[Outlet]
		UIKit.UITextField PriceTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CoffeeRoomNameLabel != null) {
				CoffeeRoomNameLabel.Dispose ();
				CoffeeRoomNameLabel = null;
			}

			if (PriceTextField != null) {
				PriceTextField.Dispose ();
				PriceTextField = null;
			}

			if (DiscountPriceTextField != null) {
				DiscountPriceTextField.Dispose ();
				DiscountPriceTextField = null;
			}
		}
	}
}

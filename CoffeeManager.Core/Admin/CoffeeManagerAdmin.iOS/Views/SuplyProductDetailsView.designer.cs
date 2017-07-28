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
	[Register ("SuplyProductDetailsView")]
	partial class SuplyProductDetailsView
	{
		[Outlet]
		UIKit.UIButton DeleteButton { get; set; }

		[Outlet]
		UIKit.UITextField ItemCountText { get; set; }

		[Outlet]
		UIKit.UITextField NameText { get; set; }

		[Outlet]
		UIKit.UILabel SalePriceLabel { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }

		[Outlet]
		UIKit.UITextField SuplyPriceText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ItemCountText != null) {
				ItemCountText.Dispose ();
				ItemCountText = null;
			}

			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}

			if (SalePriceLabel != null) {
				SalePriceLabel.Dispose ();
				SalePriceLabel = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}

			if (SuplyPriceText != null) {
				SuplyPriceText.Dispose ();
				SuplyPriceText = null;
			}

			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}
		}
	}
}

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
	[Register ("OrderItemViewCell")]
	partial class OrderItemViewCell
	{
		[Outlet]
		UIKit.UIImageView DoneImage { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint DoneImageNormarHeight { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint DoneImageZeroHeight { get; set; }

		[Outlet]
		UIKit.UILabel NameText { get; set; }

		[Outlet]
		UIKit.UILabel PriceLabel { get; set; }

		[Outlet]
		UIKit.UITextField QuantityText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DoneImage != null) {
				DoneImage.Dispose ();
				DoneImage = null;
			}

			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}

			if (PriceLabel != null) {
				PriceLabel.Dispose ();
				PriceLabel = null;
			}

			if (QuantityText != null) {
				QuantityText.Dispose ();
				QuantityText = null;
			}

			if (DoneImageZeroHeight != null) {
				DoneImageZeroHeight.Dispose ();
				DoneImageZeroHeight = null;
			}

			if (DoneImageNormarHeight != null) {
				DoneImageNormarHeight.Dispose ();
				DoneImageNormarHeight = null;
			}
		}
	}
}

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
	[Register ("ProductItemCell")]
	partial class ProductItemCell
	{
		[Outlet]
		UIKit.UILabel CategoryLabel { get; set; }

		[Outlet]
		UIKit.UISwitch IsActiveSwitch { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CategoryLabel != null) {
				CategoryLabel.Dispose ();
				CategoryLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (IsActiveSwitch != null) {
				IsActiveSwitch.Dispose ();
				IsActiveSwitch = null;
			}
		}
	}
}

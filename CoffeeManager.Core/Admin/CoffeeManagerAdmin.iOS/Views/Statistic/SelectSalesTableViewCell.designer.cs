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
	[Register ("SelectSalesTableViewCell")]
	partial class SelectSalesTableViewCell
	{
		[Outlet]
		UIKit.UISwitch IsSelectedSwitch { get; set; }

		[Outlet]
		UIKit.UILabel SaleNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SaleNameLabel != null) {
				SaleNameLabel.Dispose ();
				SaleNameLabel = null;
			}

			if (IsSelectedSwitch != null) {
				IsSelectedSwitch.Dispose ();
				IsSelectedSwitch = null;
			}
		}
	}
}

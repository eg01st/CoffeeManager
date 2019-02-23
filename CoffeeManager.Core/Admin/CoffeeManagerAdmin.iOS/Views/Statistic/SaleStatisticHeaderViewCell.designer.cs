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
	[Register ("SaleStatisticHeaderViewCell")]
	partial class SaleStatisticHeaderViewCell
	{
		[Outlet]
		UIKit.UILabel AmountLabel { get; set; }

		[Outlet]
		UIKit.UIImageView CollapseImageView { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AmountLabel != null) {
				AmountLabel.Dispose ();
				AmountLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (CollapseImageView != null) {
				CollapseImageView.Dispose ();
				CollapseImageView = null;
			}
		}
	}
}

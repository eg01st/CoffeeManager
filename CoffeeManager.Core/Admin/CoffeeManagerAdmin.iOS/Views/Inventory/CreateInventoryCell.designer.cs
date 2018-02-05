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
	[Register ("CreateInventoryCell")]
	partial class CreateInventoryCell
	{
		[Outlet]
		UIKit.UILabel CurrentCountLabel { get; set; }

		[Outlet]
		UIKit.UIImageView IsSelectedImage { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel PreviosCountLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CurrentCountLabel != null) {
				CurrentCountLabel.Dispose ();
				CurrentCountLabel = null;
			}

			if (IsSelectedImage != null) {
				IsSelectedImage.Dispose ();
				IsSelectedImage = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (PreviosCountLabel != null) {
				PreviosCountLabel.Dispose ();
				PreviosCountLabel = null;
			}
		}
	}
}

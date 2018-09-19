// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
	[Register ("AddProductView")]
	partial class AddProductView
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		UIKit.UITextField ProductNameTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (ProductNameTextField != null) {
				ProductNameTextField.Dispose ();
				ProductNameTextField = null;
			}
		}
	}
}

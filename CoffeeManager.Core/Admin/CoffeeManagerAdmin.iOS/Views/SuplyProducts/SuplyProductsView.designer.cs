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
	[Register ("SuplyProductsView")]
	partial class SuplyProductsView
	{
		[Outlet]
		UIKit.UITextField CoffeeRoomTextField { get; set; }

		[Outlet]
		UIKit.UIView ContainerView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (CoffeeRoomTextField != null) {
				CoffeeRoomTextField.Dispose ();
				CoffeeRoomTextField = null;
			}
		}
	}
}

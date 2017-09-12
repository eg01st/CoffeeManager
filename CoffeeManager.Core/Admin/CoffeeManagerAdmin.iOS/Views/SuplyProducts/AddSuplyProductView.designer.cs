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
	[Register ("AddSuplyProductView")]
	partial class AddSuplyProductView
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		UIKit.UITextField SuplyProductTextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SuplyProductTextView != null) {
				SuplyProductTextView.Dispose ();
				SuplyProductTextView = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
		}
	}
}

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
	[Register ("SelectSuplyProductsForAutoOrderView")]
	partial class SelectSuplyProductsForAutoOrderView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.UIView TableContainerView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (TableContainerView != null) {
				TableContainerView.Dispose ();
				TableContainerView = null;
			}
		}
	}
}

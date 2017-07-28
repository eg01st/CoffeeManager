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
	[Register ("StatisticView")]
	partial class StatisticView
	{
		[Outlet]
		UIKit.UIButton DoneButton { get; set; }

		[Outlet]
		UIKit.UITextField FromTextField { get; set; }

		[Outlet]
		UIKit.UITextField ToTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FromTextField != null) {
				FromTextField.Dispose ();
				FromTextField = null;
			}

			if (ToTextField != null) {
				ToTextField.Dispose ();
				ToTextField = null;
			}

			if (DoneButton != null) {
				DoneButton.Dispose ();
				DoneButton = null;
			}
		}
	}
}

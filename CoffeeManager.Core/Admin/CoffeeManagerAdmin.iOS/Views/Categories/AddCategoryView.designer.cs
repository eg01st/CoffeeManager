// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
	[Register ("AddCategoryView")]
	partial class AddCategoryView
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		UIKit.UITextField NameTextFiled { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameTextFiled != null) {
				NameTextFiled.Dispose ();
				NameTextFiled = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
		}
	}
}

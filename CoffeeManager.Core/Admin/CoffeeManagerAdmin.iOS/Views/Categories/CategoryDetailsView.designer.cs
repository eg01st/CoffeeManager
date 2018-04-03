// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
	[Register ("CategoryDetailsView")]
	partial class CategoryDetailsView
	{
		[Outlet]
		UIKit.UIButton AddSubCategoryButton { get; set; }

		[Outlet]
		UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		UIKit.UITableView SubCategoriesTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SubCategoriesTableView != null) {
				SubCategoriesTableView.Dispose ();
				SubCategoriesTableView = null;
			}

			if (AddSubCategoryButton != null) {
				AddSubCategoryButton.Dispose ();
				AddSubCategoryButton = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}
		}
	}
}

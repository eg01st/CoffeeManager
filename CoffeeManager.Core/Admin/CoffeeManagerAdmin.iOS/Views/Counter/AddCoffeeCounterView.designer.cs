// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    [Register ("AddCoffeeCounterView")]
    partial class AddCoffeeCounterView
	{
		[Outlet]
		UIKit.UITextField CategoryTextField { get; set; }

		[Outlet]
		UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		UIKit.UIButton SelectCategoryButton { get; set; }

		[Outlet]
		UIKit.UIButton SelectSuplyProductButton { get; set; }

		[Outlet]
		UIKit.UITextField SuplyProductTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (CategoryTextField != null) {
				CategoryTextField.Dispose ();
				CategoryTextField = null;
			}

			if (SelectCategoryButton != null) {
				SelectCategoryButton.Dispose ();
				SelectCategoryButton = null;
			}

			if (SelectSuplyProductButton != null) {
				SelectSuplyProductButton.Dispose ();
				SelectSuplyProductButton = null;
			}

			if (SuplyProductTextField != null) {
				SuplyProductTextField.Dispose ();
				SuplyProductTextField = null;
			}
		}
	}
}

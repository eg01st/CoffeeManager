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
	[Register ("AutoOrderDetailsView")]
	partial class AutoOrderDetailsView
	{
		[Outlet]
		UIKit.UIButton AddSuplyProductsButton { get; set; }

		[Outlet]
		UIKit.UITextField CCTextField { get; set; }

		[Outlet]
		UIKit.UITextField EmailTextField { get; set; }

		[Outlet]
		UIKit.UITableView OrderItemsTableView { get; set; }

		[Outlet]
		UIKit.UITextField OrderTimeTextField { get; set; }

		[Outlet]
		UIKit.UITextField OrderWeekDayTextField { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextField { get; set; }

		[Outlet]
		UIKit.UITextField SenderEmailTextField { get; set; }

		[Outlet]
		UIKit.UITextField SubjectTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddSuplyProductsButton != null) {
				AddSuplyProductsButton.Dispose ();
				AddSuplyProductsButton = null;
			}

			if (CCTextField != null) {
				CCTextField.Dispose ();
				CCTextField = null;
			}

			if (EmailTextField != null) {
				EmailTextField.Dispose ();
				EmailTextField = null;
			}

			if (OrderItemsTableView != null) {
				OrderItemsTableView.Dispose ();
				OrderItemsTableView = null;
			}

			if (OrderTimeTextField != null) {
				OrderTimeTextField.Dispose ();
				OrderTimeTextField = null;
			}

			if (OrderWeekDayTextField != null) {
				OrderWeekDayTextField.Dispose ();
				OrderWeekDayTextField = null;
			}

			if (PasswordTextField != null) {
				PasswordTextField.Dispose ();
				PasswordTextField = null;
			}

			if (SenderEmailTextField != null) {
				SenderEmailTextField.Dispose ();
				SenderEmailTextField = null;
			}

			if (SubjectTextField != null) {
				SubjectTextField.Dispose ();
				SubjectTextField = null;
			}
		}
	}
}

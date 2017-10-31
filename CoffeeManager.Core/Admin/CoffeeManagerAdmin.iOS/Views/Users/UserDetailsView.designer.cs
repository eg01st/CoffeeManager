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
	[Register ("UserDetailsView")]
	partial class UserDetailsView
	{
		[Outlet]
		UIKit.UITextField CoffeeRoomTextField { get; set; }

		[Outlet]
		UIKit.UILabel CurrentSalaryLabel { get; set; }

		[Outlet]
		UIKit.UITextField DayPercentageTextField { get; set; }

		[Outlet]
		UIKit.UILabel EntireSalaryLabel { get; set; }

		[Outlet]
		UIKit.UITextField ExpenseTypeTextField { get; set; }

		[Outlet]
		UIKit.UITextField MinimimPaymentTextField { get; set; }

		[Outlet]
		UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		UIKit.UITextField NightPercentageTextField { get; set; }

		[Outlet]
		UIKit.UIButton PaySalaryButton { get; set; }

		[Outlet]
		UIKit.UIButton PenaltyButton { get; set; }

		[Outlet]
		UIKit.UITableView PenaltyTableView { get; set; }

		[Outlet]
		UIKit.UITextField SalaryRateTextField { get; set; }

		[Outlet]
		UIKit.UIButton UserEarningsButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CurrentSalaryLabel != null) {
				CurrentSalaryLabel.Dispose ();
				CurrentSalaryLabel = null;
			}

			if (DayPercentageTextField != null) {
				DayPercentageTextField.Dispose ();
				DayPercentageTextField = null;
			}

			if (EntireSalaryLabel != null) {
				EntireSalaryLabel.Dispose ();
				EntireSalaryLabel = null;
			}

			if (ExpenseTypeTextField != null) {
				ExpenseTypeTextField.Dispose ();
				ExpenseTypeTextField = null;
			}

			if (MinimimPaymentTextField != null) {
				MinimimPaymentTextField.Dispose ();
				MinimimPaymentTextField = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (NightPercentageTextField != null) {
				NightPercentageTextField.Dispose ();
				NightPercentageTextField = null;
			}

			if (PaySalaryButton != null) {
				PaySalaryButton.Dispose ();
				PaySalaryButton = null;
			}

			if (PenaltyButton != null) {
				PenaltyButton.Dispose ();
				PenaltyButton = null;
			}

			if (PenaltyTableView != null) {
				PenaltyTableView.Dispose ();
				PenaltyTableView = null;
			}

			if (SalaryRateTextField != null) {
				SalaryRateTextField.Dispose ();
				SalaryRateTextField = null;
			}

			if (UserEarningsButton != null) {
				UserEarningsButton.Dispose ();
				UserEarningsButton = null;
			}

			if (CoffeeRoomTextField != null) {
				CoffeeRoomTextField.Dispose ();
				CoffeeRoomTextField = null;
			}
		}
	}
}

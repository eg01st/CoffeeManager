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
	[Register ("SuplyProductDetailsView")]
	partial class SuplyProductDetailsView
	{
		[Outlet]
		UIKit.UIButton DeleteButton { get; set; }

		[Outlet]
		UIKit.UITextField ExpenseNumerationMultyplierTextField { get; set; }

		[Outlet]
		UIKit.UITextField ExpenseNumerationNameTextField { get; set; }

		[Outlet]
		UIKit.UISwitch InventoryNeededSwitch { get; set; }

		[Outlet]
		UIKit.UITextField InventoryNumerationMultiplierTextField { get; set; }

		[Outlet]
		UIKit.UITextField InventoryNumerationNameTextField { get; set; }

		[Outlet]
		UIKit.UITextField ItemCountText { get; set; }

		[Outlet]
		UIKit.UITextField NameText { get; set; }

		[Outlet]
		UIKit.UILabel SalePriceLabel { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }

		[Outlet]
		UIKit.UITextField SuplyPriceText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}

			if (ExpenseNumerationMultyplierTextField != null) {
				ExpenseNumerationMultyplierTextField.Dispose ();
				ExpenseNumerationMultyplierTextField = null;
			}

			if (ExpenseNumerationNameTextField != null) {
				ExpenseNumerationNameTextField.Dispose ();
				ExpenseNumerationNameTextField = null;
			}

			if (InventoryNeededSwitch != null) {
				InventoryNeededSwitch.Dispose ();
				InventoryNeededSwitch = null;
			}

			if (ItemCountText != null) {
				ItemCountText.Dispose ();
				ItemCountText = null;
			}

			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}

			if (SalePriceLabel != null) {
				SalePriceLabel.Dispose ();
				SalePriceLabel = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}

			if (SuplyPriceText != null) {
				SuplyPriceText.Dispose ();
				SuplyPriceText = null;
			}

			if (InventoryNumerationNameTextField != null) {
				InventoryNumerationNameTextField.Dispose ();
				InventoryNumerationNameTextField = null;
			}

			if (InventoryNumerationMultiplierTextField != null) {
				InventoryNumerationMultiplierTextField.Dispose ();
				InventoryNumerationMultiplierTextField = null;
			}
		}
	}
}

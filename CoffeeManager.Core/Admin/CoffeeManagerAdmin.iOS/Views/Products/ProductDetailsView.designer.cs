// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
	[Register ("ProductDetailsView")]
	partial class ProductDetailsView
	{
		[Outlet]
		UIKit.UIButton AddProductButton { get; set; }

		[Outlet]
		UIKit.UITextField CupTypeCategoryText { get; set; }

		[Outlet]
		UIKit.UITextField DescriptionTextField { get; set; }

		[Outlet]
		UIKit.UISwitch IsSaleByWeightSwitch { get; set; }

		[Outlet]
		UIKit.UITextField NameText { get; set; }

		[Outlet]
		UIKit.UITextField PolicePriceText { get; set; }

		[Outlet]
		UIKit.UILabel PolicePriceTitleLabel { get; set; }

		[Outlet]
		UIKit.UITextField PriceText { get; set; }

		[Outlet]
		UIKit.UILabel PriceTitleLabel { get; set; }

		[Outlet]
		UIKit.UITextField ProductTypeText { get; set; }

		[Outlet]
		UIKit.UITextField SelectedColorTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddProductButton != null) {
				AddProductButton.Dispose ();
				AddProductButton = null;
			}

			if (CupTypeCategoryText != null) {
				CupTypeCategoryText.Dispose ();
				CupTypeCategoryText = null;
			}

			if (DescriptionTextField != null) {
				DescriptionTextField.Dispose ();
				DescriptionTextField = null;
			}

			if (IsSaleByWeightSwitch != null) {
				IsSaleByWeightSwitch.Dispose ();
				IsSaleByWeightSwitch = null;
			}

			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}

			if (PolicePriceText != null) {
				PolicePriceText.Dispose ();
				PolicePriceText = null;
			}

			if (PolicePriceTitleLabel != null) {
				PolicePriceTitleLabel.Dispose ();
				PolicePriceTitleLabel = null;
			}

			if (PriceText != null) {
				PriceText.Dispose ();
				PriceText = null;
			}

			if (PriceTitleLabel != null) {
				PriceTitleLabel.Dispose ();
				PriceTitleLabel = null;
			}

			if (SelectedColorTextField != null) {
				SelectedColorTextField.Dispose ();
				SelectedColorTextField = null;
			}

			if (ProductTypeText != null) {
				ProductTypeText.Dispose ();
				ProductTypeText = null;
			}
		}
	}
}

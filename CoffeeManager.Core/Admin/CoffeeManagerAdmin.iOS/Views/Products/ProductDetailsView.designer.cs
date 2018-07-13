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
		UIKit.UIButton AddPaymentStrategyButton { get; set; }

		[Outlet]
		UIKit.UIButton AddProductButton { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint BottomButtonContraint { get; set; }

		[Outlet]
		UIKit.UITextField CupTypeCategoryText { get; set; }

		[Outlet]
		UIKit.UITextField DescriptionTextField { get; set; }

		[Outlet]
		UIKit.UISwitch IsPaymentPercentStrategySwich { get; set; }

		[Outlet]
		UIKit.UISwitch IsSaleByWeightSwitch { get; set; }

		[Outlet]
		UIKit.UITextField NameText { get; set; }

		[Outlet]
		UIKit.UITableView PaymentStrategyTableView { get; set; }

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
			if (AddPaymentStrategyButton != null) {
				AddPaymentStrategyButton.Dispose ();
				AddPaymentStrategyButton = null;
			}

			if (AddProductButton != null) {
				AddProductButton.Dispose ();
				AddProductButton = null;
			}

			if (BottomButtonContraint != null) {
				BottomButtonContraint.Dispose ();
				BottomButtonContraint = null;
			}

			if (CupTypeCategoryText != null) {
				CupTypeCategoryText.Dispose ();
				CupTypeCategoryText = null;
			}

			if (DescriptionTextField != null) {
				DescriptionTextField.Dispose ();
				DescriptionTextField = null;
			}

			if (IsPaymentPercentStrategySwich != null) {
				IsPaymentPercentStrategySwich.Dispose ();
				IsPaymentPercentStrategySwich = null;
			}

			if (IsSaleByWeightSwitch != null) {
				IsSaleByWeightSwitch.Dispose ();
				IsSaleByWeightSwitch = null;
			}

			if (NameText != null) {
				NameText.Dispose ();
				NameText = null;
			}

			if (PaymentStrategyTableView != null) {
				PaymentStrategyTableView.Dispose ();
				PaymentStrategyTableView = null;
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

			if (ProductTypeText != null) {
				ProductTypeText.Dispose ();
				ProductTypeText = null;
			}

			if (SelectedColorTextField != null) {
				SelectedColorTextField.Dispose ();
				SelectedColorTextField = null;
			}
		}
	}
}

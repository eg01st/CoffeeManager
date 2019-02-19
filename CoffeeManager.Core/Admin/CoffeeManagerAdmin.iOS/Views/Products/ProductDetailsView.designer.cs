// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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
        UIKit.NSLayoutConstraint PaymentStrategyHeightConstraint { get; set; }


        [Outlet]
        UIKit.UITableView PaymentStrategyTableView { get; set; }


        [Outlet]
        UIKit.UILabel PriceLabel { get; set; }


        [Outlet]
        UIKit.UITableView PriceTableView { get; set; }


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

            if (PaymentStrategyHeightConstraint != null) {
                PaymentStrategyHeightConstraint.Dispose ();
                PaymentStrategyHeightConstraint = null;
            }

            if (PaymentStrategyTableView != null) {
                PaymentStrategyTableView.Dispose ();
                PaymentStrategyTableView = null;
            }

            if (PriceLabel != null) {
                PriceLabel.Dispose ();
                PriceLabel = null;
            }

            if (PriceTableView != null) {
                PriceTableView.Dispose ();
                PriceTableView = null;
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
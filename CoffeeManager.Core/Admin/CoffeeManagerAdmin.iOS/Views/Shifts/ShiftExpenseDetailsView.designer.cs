// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//

using Foundation;

namespace CoffeeManagerAdmin.iOS.Views.Shifts
{
    [Register ("ShiftExpenseDetailsView")]
    partial class ShiftExpenseDetailsView
    {
        [Outlet]
        UIKit.UITableView ExpensesTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ExpensesTableView != null) {
                ExpensesTableView.Dispose ();
                ExpensesTableView = null;
            }
        }
    }
}
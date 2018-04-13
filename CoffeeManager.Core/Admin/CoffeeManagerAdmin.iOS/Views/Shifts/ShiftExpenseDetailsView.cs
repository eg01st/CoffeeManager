using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS.Views.Shifts
{
    public partial class ShiftExpenseDetailsView : ViewControllerBase<ShiftExpenseDetailsViewModel>
    {
        public ShiftExpenseDetailsView() : base("ShiftExpenseDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Детали расхода";
            var source = new SimpleTableSource(ExpensesTableView, ShiftExpenseTableViewCell.Key, ShiftExpenseTableViewCell.Nib, ShiftExpenseTableHeader.Key, ShiftExpenseTableHeader.Nib);
            ExpensesTableView.Source = source;

            var set = this.CreateBindingSet<ShiftExpenseDetailsView, ShiftExpenseDetailsViewModel>();
            set.Bind(source).To(vm => vm.Items);
            set.Apply();
        }
    }
}


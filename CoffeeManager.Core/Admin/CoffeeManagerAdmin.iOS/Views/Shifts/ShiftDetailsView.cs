using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftDetailsView : ViewControllerBase<ShiftDetailsViewModel>
    {
        public ShiftDetailsView() : base("ShiftDetailsView", null)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Детали смены";
            var source = new SimpleTableSource(ExpenseTableView, ExpenseItemCell.Key, ExpenseItemCell.Nib);
            ExpenseTableView.Source = source;
       
            var set = this.CreateBindingSet<ShiftDetailsView, ShiftDetailsViewModel>();
            set.Bind(CopSalePercentageLabel).To(vm => vm.CopSalePercentage);
            set.Bind(source).To(vm => vm.ExpenseItems);
            set.Bind(UserNameButton).For("Title").To(vm => vm.Name);
            set.Bind(UserNameButton).To(vm => vm.ShowUserDetailsCommand);
            set.Bind(DateLabel).To(vm => vm.Date);
            set.Bind(CoffeeCounter).To(vm => vm.Counter);
            set.Bind(CoffeeSaleCounter).To(vm => vm.UsedCoffee);
            set.Bind(RejectedSalesLabel).To(vm => vm.RejectedSales);
            set.Bind(UtilizedSalesLabel).To(vm => vm.UtilizedSales);
            set.Bind(SalesButton).To(vm => vm.ShowSalesCommand);
            set.Bind(AddExpenseButton).For(b => b.Hidden).To(vm => vm.IsFinished);
            set.Bind(AddExpenseButton).To(vm => vm.AddExpenseCommand);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


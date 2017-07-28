using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ShiftDetailsView : MvxViewController
    {
        public ShiftDetailsView() : base("ShiftDetailsView", null)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Детали смены";
            var source = new ExpenseTableSource(ExpenseTableView);
            ExpenseTableView.Source = source;
       
            var set = this.CreateBindingSet<ShiftDetailsView, ShiftDetailsViewModel>();
            set.Bind(CopSalePercentageLabel).To(vm => vm.CopSalePercentage);
            set.Bind(source).To(vm => vm.ExpenseItems);
            set.Bind(NameLabel).To(vm => vm.Name);
            set.Bind(DateLabel).To(vm => vm.Date);
            set.Bind(CoffeeCounter).To(vm => vm.Counter);
            set.Bind(CoffeeSaleCounter).To(vm => vm.UsedCoffee);
            set.Bind(RejectedSalesLabel).To(vm => vm.RejectedSales);
            set.Bind(UtilizedSalesLabel).To(vm => vm.UtilizedSales);
            set.Bind(SalesButton).To(vm => vm.ShowSalesCommand);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}


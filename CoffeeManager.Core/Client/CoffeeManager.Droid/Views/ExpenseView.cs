using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustResize, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class ExpenseView : ActivityWithToolbar<ExpenseViewModel>
    {
        [FindById(Resource.Id.add_expense_button)]
        private Button addExpenseButton;
        
        protected override string GetToolbarTitle() => "Расходы";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public ExpenseView() : base(Resource.Layout.exprense_layout)
        {
        }

        public bool IsAddButtomEnabled
        {
            set => addExpenseButton.Alpha = value ? 1 : 0.3f;
        }
        

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<ExpenseView, ExpenseViewModel>();
            set.Bind(this).For(nameof(IsAddButtomEnabled)).To(vm => vm.IsAddButtomEnabled);
            set.Apply();
        }
    }
}
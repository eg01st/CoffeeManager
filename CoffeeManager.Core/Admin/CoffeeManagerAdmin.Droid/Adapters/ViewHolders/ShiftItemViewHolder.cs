using Android.Views;
using Android.Widget;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManagerAdmin.Droid.Adapters.ViewHolders
{
    public class ShiftItemViewHolder : CardViewHolder
    {
        public ShiftItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.date)] private TextView Date { get; set; }

        [FindById(Resource.Id.name)] private TextView UserName { get; set; }
        [FindById(Resource.Id.expenses)] private TextView Expenses { get; set; }
        [FindById(Resource.Id.earned)] private TextView EarnedAmount { get; set; }
        [FindById(Resource.Id.read_shift)] private TextView ReadShift { get; set; }
        [FindById(Resource.Id.card)] private TextView Card { get; set; }
        [FindById(Resource.Id.real_amount)] private TextView ReadAmount { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<ShiftItemViewHolder, ShiftItemViewModel>();
            bindingSet.Bind(Date).To(vm => vm.Date);
            bindingSet.Bind(UserName).To(vm => vm.UserName);
            bindingSet.Bind(Expenses).To(vm => vm.ExpenseAmount);
            bindingSet.Bind(EarnedAmount).To(vm => vm.EarnedAmount);
            bindingSet.Bind(ReadShift).To(vm => vm.RealShiftAmount);
            bindingSet.Bind(Card).To(vm => vm.CreditCardAmount);
            bindingSet.Bind(ReadAmount).To(vm => vm.RealAmount);
            bindingSet.Apply();
        }
    }
}
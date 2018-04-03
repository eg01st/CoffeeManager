using Android.Views;
using Android.Widget;
using CoffeeManagerAdmin.Core.ViewModels.CreditCard;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManagerAdmin.Droid.Adapters.ViewHolders
{
    public class CashoutHistoryItemViewHolder: CardViewHolder
    {
        public CashoutHistoryItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.date)] private TextView Date { get; set; }

        [FindById(Resource.Id.amount)] private TextView Amount { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<CashoutHistoryItemViewHolder, CashoutHistoryItemViewModel>();
            bindingSet.Bind(Date).To(vm => vm.Date);
            bindingSet.Bind(Amount).To(vm => vm.Amount);
            bindingSet.Apply();
        }
    }
}
using Android.Views;
using Android.Widget;
using CoffeeManagerAdmin.Core;
using Java.Util.Jar;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Common;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManagerAdmin.Droid.Adapters.ViewHolders
{
    public class UserEarningViewHolder: CardViewHolder
    {
        public UserEarningViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.date)] private TextView Date { get; set; }

        [FindById(Resource.Id.amount)] private TextView Amount { get; set; }
        
        [FindById(Resource.Id.shift)] private TextView Shift { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<UserEarningViewHolder, UserEarningItemViewModel>();
            bindingSet.Bind(Date).To(vm => vm.Date);
            bindingSet.Bind(Amount).To(vm => vm.Amount);
            bindingSet.Bind(Shift).To(vm => vm.ShiftType);
            bindingSet.Apply();
        }
    }
}
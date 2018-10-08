using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels.Motivation;
using CoffeeManager.Droid.Converters;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManager.Droid.Adapters.ViewHolders
{
    public class MotivationViewHolder : CardViewHolder
    {
        public MotivationViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.name_text_view)] private TextView Name { get; set; }

        [FindById(Resource.Id.shift_text_view)] private TextView Shift { get; set; }

        [FindById(Resource.Id.money_text_view)] private TextView Money { get; set; }

        [FindById(Resource.Id.entire_text_view)] private TextView Entire { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<MotivationViewHolder, MotivationItemViewModel>();
            bindingSet.Bind(Name).To(vm => vm.UserName);
            bindingSet.Bind(Shift).To(vm => vm.ShiftScore);
            bindingSet.Bind(Money).To(vm => vm.MoneyScore);
            bindingSet.Bind(Entire).To(vm => vm.EntireScore);
            bindingSet.Apply();
        }
    }
}
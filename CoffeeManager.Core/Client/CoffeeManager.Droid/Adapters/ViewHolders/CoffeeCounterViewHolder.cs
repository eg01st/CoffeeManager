using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels.CoffeeCounter;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManager.Droid.Adapters.ViewHolders
{
    public class CoffeeCounterViewHolder : CardViewHolder
    {
        public CoffeeCounterViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }
        
        [FindById(Resource.Id.name_text_view)] private TextView Name { get; set; }
        
        [FindById(Resource.Id.start_counter_edit_text)] private TextInputEditText Counter { get; set; }

        [FindById(Resource.Id.confirm_edit_text)] private TextInputEditText Confirm { get; set; }


        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<CoffeeCounterViewHolder, CoffeeCounterItemViewModel>();
            bindingSet.Bind(Name).To(vm => vm.Name);
            bindingSet.Bind(Counter).To(vm => vm.Counter);
            bindingSet.Bind(Confirm).To(vm => vm.Confirm);
            bindingSet.Apply();
        }
    }
}

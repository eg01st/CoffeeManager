using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels.Inventory;
using CoffeeManager.Droid.Converters;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManager.Droid.Adapters.ViewHolders
{
    public class PartialInventoryItemViewHolder: CardViewHolder
    {
        public PartialInventoryItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }
        
        [FindById(Resource.Id.name_text_view)] private TextView Name { get; set; }
        
        [FindById(Resource.Id.quantity_text_view)] private TextInputEditText Quantity { get; set; }

        [FindById(Resource.Id.quantity_name_text_view)] private TextView QuantityName { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<PartialInventoryItemViewHolder, PartialInventoryItemViewModel>();
            bindingSet.Bind(Name).To(vm => vm.Name);
            bindingSet.Bind(Quantity).To(vm => vm.Quantity).WithConversion(new IntToStringConverter());
            bindingSet.Bind(QuantityName).To(vm => vm.ExpenseNumerationName);
            bindingSet.Apply();
        }
    }
}
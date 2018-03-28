using Android.Views;
using Android.Widget;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManagerAdmin.Droid.Adapters.ViewHolders
{
    public class CoffeeRoomItemViewHolder: CardViewHolder
    {
        public CoffeeRoomItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.store_name)] private TextView Name { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<CoffeeRoomItemViewHolder, CoffeeRoomItemViewModel>();
            bindingSet.Bind(Name).To(vm => vm.Name);
            bindingSet.Apply();
        }
    }
}
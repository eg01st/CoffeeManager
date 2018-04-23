using Android.Views;
using Android.Widget;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Common;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManagerAdmin.Droid.Adapters.ViewHolders
{
    public class UserItemViewHolder : CardViewHolder
    {
        public UserItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.name_text)] private TextView Name { get; set; }

        [FindById(Resource.Id.is_active_checkbox)] private CheckBox IsActive { get; set; }

        public override void BindData()
        {
            var bindingSet = this.CreateBindingSet<UserItemViewHolder, UserItemViewModel>();
            bindingSet.Bind(Name).To(vm => vm.UserName);
            bindingSet.Bind(IsActive).For(c => c.Checked).To(vm => vm.IsActive);
            bindingSet.Bind(IsActive).For(BindingConstants.Click).To(vm => vm.ToggleIsActiveCommand);
            bindingSet.Apply();
        }
    }
    
}

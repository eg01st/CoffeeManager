using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using CoffeeManagerAdmin.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Users
{
    [MvxActivityPresentation]
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
    public class UserDetailView: ActivityWithToolbar<UserDetailsViewModel>
    {
        protected override int GetMenuResourceId() => Resource.Menu.user_details_menu;

        protected override int GetToolbarTitleStringResourceId() => Resource.String.user_detail;

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        [FindById(Resource.Id.recyclerview_penalties)]
        private EndlessRecyclerView usersRecyclerView;

        
        public UserDetailView() : base(Resource.Layout.user_detail)
        {
        }
        
        
        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            InitRecyclerView();
        }
        
        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<UserDetailView, UserDetailsViewModel>();
            bindingSet.Bind(usersRecyclerView).For(v => v.ItemsSource).To(vm => vm.Penalties);
            bindingSet.Bind(usersRecyclerView.Adapter).For(v => v.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            usersRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<UserPenaltyItemViewModel, PenaltyItemViewHolder>(Resource.Layout.penalty_item);
            usersRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.save)
            {
                ViewModel.UpdateCommand.Execute(null);
                return true;
            }
            
            return base.OnOptionsItemSelected(item);
        }
    }
}
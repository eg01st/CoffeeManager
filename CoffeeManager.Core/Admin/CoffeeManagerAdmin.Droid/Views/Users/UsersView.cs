using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
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
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class UsersView : ActivityWithToolbar<UsersViewModel>
    {
        protected override int GetToolbarTitleStringResourceId() => Resource.String.users;

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        [FindById(Resource.Id.recyclerview_users)]
        private EndlessRecyclerView usersRecyclerView;


        public UsersView() : base(Resource.Layout.users)
        {
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            InitRecyclerView();
        }
        
        
        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<UsersView, UsersViewModel>();
            bindingSet.Bind(usersRecyclerView).For(v => v.ItemsSource).To(vm => vm.Users);
            bindingSet.Bind(usersRecyclerView.Adapter).For(v => v.ItemClick).To(vm => vm.ItemSelectedCommand);
              bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            usersRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<UserItemViewModel, UserItemViewHolder>(Resource.Layout.user_item);
            usersRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }
    }
}
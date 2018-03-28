using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Home;
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
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class UserEarningsView : ActivityWithToolbar<UserEarningsViewModel>
    {
        protected override int GetToolbarTitleStringResourceId() => Resource.String.payments;

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        
        [FindById(Resource.Id.recyclerview_earnings)]
        private EndlessRecyclerView earningsRecyclerView;

        
        public UserEarningsView() : base(Resource.Layout.user_earnings)
        {
            
        }
        
        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            InitRecyclerView();
        }
        
        
        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<UserEarningsView, UserEarningsViewModel>();
            bindingSet.Bind(earningsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(earningsRecyclerView.Adapter).For(v => v.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            earningsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<UserEarningItemViewModel, UserEarningViewHolder>(Resource.Layout.user_earnings_item);
            earningsRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }
    }
}
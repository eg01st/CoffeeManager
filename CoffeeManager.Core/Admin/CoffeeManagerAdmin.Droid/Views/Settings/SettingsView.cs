using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using CoffeeManagerAdmin.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Settings
{
    [MvxActivityPresentation]
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsView : ActivityWithToolbar<SettingsViewModel>
    {
        protected override int GetToolbarTitleStringResourceId() => Resource.String.settings;

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;
        
        [FindById(Resource.Id.recyclerview_stores)]
        private EndlessRecyclerView storesRecyclerView;
        
        public SettingsView() : base(Resource.Layout.settings)
        {
        }
        
        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            InitRecyclerView();
        }
        
        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<SettingsView, SettingsViewModel>();
            bindingSet.Bind(storesRecyclerView).For(v => v.ItemsSource).To(vm => vm.CoffeeRooms);
            bindingSet.Bind(storesRecyclerView.Adapter).For(v => v.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            storesRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<CoffeeRoomItemViewModel, CoffeeRoomItemViewHolder>(Resource.Layout.store_item);
            storesRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }
    }
}
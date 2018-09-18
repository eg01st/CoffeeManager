using Android.App;
using Android.Content.PM;
using Android.OS;
using CoffeeManager.Core.ViewModels.CoffeeCounter;
using CoffeeManager.Core.ViewModels.Motivation;
using CoffeeManager.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManager.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class MotivationView : MobileCore.Droid.Activities.ActivityWithToolbar<MotivationViewModel>
    {
        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        [FindById(Resource.Id.counters_recycler)]
        private EndlessRecyclerView usersRecyclerView;
        
        public MotivationView() : base(Resource.Layout.activity_motivation)
        {
            
        }

        protected override void InitViewProperties(Bundle bundle)
        {
            base.InitViewProperties(bundle);
            usersRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<MotivationItemViewModel, MotivationViewHolder>(Resource.Layout.item_motivation);
            usersRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }

        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<MotivationView, MotivationViewModel>();
            bindingSet.Bind(usersRecyclerView.Adapter).For(v => v.ItemsSource).To(vm => vm.ItemsCollection);
            bindingSet.Apply();
        }
    }
}

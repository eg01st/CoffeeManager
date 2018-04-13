using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Core.ViewModels.CoffeeCounter;
using CoffeeManager.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManager.Droid.Views
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class EndShiftView : ActivityWithToolbar<EndShiftViewModel>
    {
        [FindById(Resource.Id.counters_recycler)]
        private EndlessRecyclerView countersRecyclerView;

        
        protected override string GetToolbarTitle() => "Окончание смены";

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        public EndShiftView() : base(Resource.Layout.end_shift)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitRecyclerView();
        }

        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<EndShiftView, EndShiftViewModel>();
            bindingSet.Bind(countersRecyclerView).For(v => v.ItemsSource).To(vm => vm.ItemsCollection);
            bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            countersRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<CoffeeCounterItemViewModel, CoffeeCounterViewHolder>(Resource.Layout.coffee_counter_item);
            countersRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }

    }
}
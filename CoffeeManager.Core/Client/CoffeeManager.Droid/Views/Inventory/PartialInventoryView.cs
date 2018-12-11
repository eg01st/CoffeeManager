using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using CoffeeManager.Core.ViewModels.CoffeeCounter;
using CoffeeManager.Core.ViewModels.Inventory;
using CoffeeManager.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;

namespace CoffeeManager.Droid.Views.Inventory
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class PartialInventoryView : MobileCore.Droid.Activities.ActivityBase<PartialInventoryViewModel>
    {
        [FindById(Resource.Id.counters_recycler)]
        private EndlessRecyclerView countersRecyclerView;
        
        [FindById(Resource.Id.done_button)]
        private Button confirmButton;
        
        public PartialInventoryView() : base(Resource.Layout.coffee_counter_layout)
        {
            
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitRecyclerView();
        }

        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<PartialInventoryView, PartialInventoryViewModel>();
            bindingSet.Bind(countersRecyclerView).For(v => v.ItemsSource).To(vm => vm.ItemsCollection);
            bindingSet.Bind(confirmButton).To(vm => vm.CloseCommand);
            bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            countersRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<PartialInventoryItemViewModel, PartialInventoryItemViewHolder>(Resource.Layout.partial_inventory_item);
            countersRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }
    }
}
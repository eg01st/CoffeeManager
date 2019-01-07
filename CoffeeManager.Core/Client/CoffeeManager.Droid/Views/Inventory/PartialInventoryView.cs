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
using MvvmCross.Binding.Droid;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Droid.Views.Inventory
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class PartialInventoryView : MobileCore.Droid.Activities.ActivityBase<PartialInventoryViewModel>
    {
        [FindById(Resource.Id.items_recycler)]
        private EndlessRecyclerView countersRecyclerView;

        [FindById(Resource.Id.done_button)]
        private Button confirmButton;


        public PartialInventoryView() : base(Resource.Layout.activity_partial_inventory)
        {
            
        }

        public IMvxAsyncCommand DoneCommad { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitRecyclerView();
        }


        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<PartialInventoryView, PartialInventoryViewModel>();
            bindingSet.Bind(countersRecyclerView).For(v => v.ItemsSource).To(vm => vm.ItemsCollection);
            bindingSet.Bind(confirmButton).For(b => b.BindClick()).To(vm => vm.DoneCommand);
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
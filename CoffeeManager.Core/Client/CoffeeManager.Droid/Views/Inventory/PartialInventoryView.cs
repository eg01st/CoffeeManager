using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
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
    public class PartialInventoryView : MobileCore.Droid.Activities.ActivityWithToolbar<PartialInventoryViewModel>
    {
        [FindById(Resource.Id.items_recycler)]
        private EndlessRecyclerView countersRecyclerView;

        protected override int GetMenuResourceId() => Resource.Menu.done_button_menu;

        public PartialInventoryView() : base(Resource.Layout.activity_partial_inventory)
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
            bindingSet.Apply();
        }

        private void InitRecyclerView()
        {
            countersRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<PartialInventoryItemViewModel, PartialInventoryItemViewHolder>(Resource.Layout.partial_inventory_item);
            countersRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemId = item.ItemId;
            if (itemId == Resource.Id.action_done)
            {
                ViewModel.DoneCommand.Execute(null);
                return true;
            }
            return false;
        }

        public override void OnBackPressed()
        {
        }
    }
}
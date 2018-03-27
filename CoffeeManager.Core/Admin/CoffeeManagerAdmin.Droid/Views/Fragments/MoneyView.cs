using Android.OS;
using Android.Runtime;
using Android.Views;
using CoffeeManagerAdmin.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.Core.ViewModels.Shifts;
using CoffeeManagerAdmin.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Common;
using MobileCore.Droid.Controls;
using MobileCore.Droid.Fragments;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.Fragments
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.home_tab_layout, ViewPagerResourceId = Resource.Id.home_viewpager,Title = nameof(MoneyView))]
    [Register(nameof(MoneyView))]
    public class MoneyView : FragmentBase<MoneyViewModel>
    {
        [FindById(Resource.Id.recyclerview_shifts)]
        private EndlessRecyclerView shiftsRecyclerView;
        
        protected override int GetToolbarId() => Resource.Id.toolbar;

        protected override int GetToolbarTitleStringId() => Resource.String.finance_tab_title;
        
        protected override bool HasNavigationIcon() => false;

        public MoneyView() : base(Resource.Layout.fragment_finance)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            InitRecyclerView();

            return view;
        }

        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<MoneyView, MoneyViewModel>();
            bindingSet.Bind(shiftsRecyclerView).For(v => v.ItemsSource).To(vm => vm.Items);
            bindingSet.Bind(shiftsRecyclerView.Adapter).For(v => v.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Bind(shiftsRecyclerView).For(BindingConstants.LoadMore).To(vm => vm.LoadNextPageCommand);
            bindingSet.Apply();
        }

        private void InitRecyclerView()
        {
            shiftsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<ShiftItemViewModel, ShiftItemViewHolder>(Resource.Layout.shift_info_item);
            shiftsRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }
    }
}
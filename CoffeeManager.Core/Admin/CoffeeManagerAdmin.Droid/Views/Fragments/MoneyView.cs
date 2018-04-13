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

        protected override int GetNavigationIconId() => Resource.Drawable.ic_settings_black_24dp;

        protected override bool HasNavigationIcon() => true;

        protected override int GetMenuResourceId() => Resource.Menu.main_menu;

        public MoneyView() : base(Resource.Layout.fragment_finance)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            HasOptionsMenu = true;
            InitRecyclerView();

            OnCreateOptionsMenu(Toolbar.Menu, Activity.MenuInflater);

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

        protected override void OnNavigationIconClick()    
        {
            ViewModel.ShowSettingsCommand.Execute(null);
        }

        private void InitRecyclerView()
        {
            shiftsRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<ShiftItemViewModel, ShiftItemViewHolder>(Resource.Layout.shift_info_item);
            shiftsRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
            shiftsRecyclerView.HasNextPage = true;
        }

        public override bool OnMenuItemClick(IMenuItem item)
        {
            var id = item.ItemId;

            if (id == Resource.Id.card)
            {
                ViewModel.ShowCreditCardCommand.Execute(null);
                return true;
            } 
            if (id == Resource.Id.user)
            {
                ViewModel.ShowUsersCommand.Execute(null);
                return true;
            }
            return base.OnMenuItemClick(item);
        }
    }
}
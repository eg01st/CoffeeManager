using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Adapters;
using CoffeeManager.Droid.Views.Fragments;
using MvvmCross.Binding.BindingContext;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using System.Linq;
using CoffeeManager.Core.ViewModels.Products;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class MainView : MobileCore.Droid.Activities.ActivityBase<MainViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private readonly int drawerGravity = GravityCompat.Start;

        [FindById(Resource.Id.main_drawer)]
        private DrawerLayout drawerLayout;
        
        [FindById(Resource.Id.main_viewpager)]
        private ViewPager viewPager;
        
        [FindById(Resource.Id.police_sale_enabled)]
        private View policeSaleView;
        
        [FindById(Resource.Id.credit_card_enabled)]
        private View creditCardView;
        
        [FindById(Resource.Id.police_sale_frame)]
        private FrameLayout policeSaleFrame;
        
        [FindById(Resource.Id.credit_card_frame)]
        private FrameLayout creditCardFrame;
        
        private TextView userNameTextView;
        private MvxObservableCollection<CategoryItemViewModel> categoies;
        private MvxObservableCollection<ProductViewModel> products;

        public MvxObservableCollection<CategoryItemViewModel> Categories
        {
            get => categoies;
            set
            {
                categoies = value;
                SetupViewPager(categoies.ToList());
            }
        }
        
        public MvxObservableCollection<ProductViewModel> Products
        {
            get => products;
            set => products = value;
        }

        public MainView() : base(Resource.Layout.main)
        {
            
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            InitLeftMenu();
            InitToolBarCommands();
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<MainView, MainViewModel>();
            set.Bind(this).For(c => c.Products).To(vm => vm.Products);
            set.Bind(this).For(c => c.Categories).To(vm => vm.Categories);
            set.Bind(this).For(c => c.SelectedCategoryId).To(vm => vm.SelectedCategoryId);
            set.Bind(userNameTextView).To(vm => vm.UserName).OneWay();
            set.Apply();
        }

        public int SelectedCategoryId
        {
            get => ViewModel.SelectedCategoryId;
            set
            {
                if (value != default(int))
                {
                    var productTab = Categories.First(p => p.Id == value);
                    var index = Categories.IndexOf(productTab);
                    viewPager.CurrentItem = index;
                }
            }
        }

        private void InitToolBarCommands()
        {
            Android.Support.V7.Widget.Toolbar toolbar = (Android.Support.V7.Widget.Toolbar) FindViewById(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
            }

            SupportActionBar.Title = string.Empty;
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        protected override void SubscribeToLayoutEvents()
        {
            policeSaleFrame.Click += PoliceSale_Click;
            creditCardFrame.Click += CreditCard_Click;
            viewPager.PageSelected += ViewPager_PageSelected;
        }

        protected override void UnSubscribeFromLayoutEvents()
        {
            policeSaleFrame.Click -= PoliceSale_Click;
            creditCardFrame.Click -= CreditCard_Click;
            viewPager.PageSelected -= ViewPager_PageSelected;
        }

        private void CreditCard_Click(object sender, EventArgs e)
        {
            ViewModel.EnableCreditCardSaleCommand.Execute(null);
            creditCardView.Visibility = ViewModel.IsCreditCardSaleEnabled ? ViewStates.Visible : ViewStates.Invisible;
        }

        private void PoliceSale_Click(object sender, System.EventArgs e)
        {
            ViewModel.EnablePoliceSaleCommand.Execute(null);
            policeSaleView.Visibility = ViewModel.IsPoliceSaleEnabled ? ViewStates.Visible : ViewStates.Invisible;
        }

        private void SetupViewPager(List<CategoryItemViewModel> categories)
        {
            var adapter = new ViewPagerAdapter(SupportFragmentManager);

            foreach (var category in categories)
            {
                var vm = ViewModel.Products.First(p => p.CategoryId == category.Id);
                var fragment = new ProductFragment();
                fragment.DataContext = vm;
                adapter.AddFragment(fragment, category.Name);
            }

            viewPager.Adapter = adapter;
            viewPager.OffscreenPageLimit = categories.Count();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);
            return true;
        }

        void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            var vm = ViewModel.Categories[e.Position];
            if (vm != null)
            {
                ViewModel.OnCategorySelectedAction(vm.Id);
            }
        }


        #region DrawerLayout

        private void InitLeftMenu()
        {
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, Resource.Drawable.ic_menu_black_24dp, Resource.Drawable.ic_keyboard_backspace_black_24dp);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            
            NavigationView navigationView = (NavigationView) FindViewById(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            
            userNameTextView = navigationView.GetHeaderView(0).FindViewById<TextView>(Resource.Id.user_name_text);
        }
        
        public override void OnBackPressed()
        {
            if (drawerLayout.IsDrawerOpen(drawerGravity) == true)
            {
                drawerLayout.CloseDrawer(drawerGravity);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemId = item.ItemId;
            if (itemId == Resource.Id.action_refresh)
            {
                ViewModel.RefreshCommand.Execute(null);
                return true;
            }
            else if (itemId == Android.Resource.Id.Home)
            {
                if (drawerLayout.IsDrawerOpen(drawerGravity) == true)
                {
                    drawerLayout.CloseDrawer(drawerGravity);
                }
                else
                {
                    drawerLayout.OpenDrawer(drawerGravity);
                }
                return true;
            }

            return false;
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            var viewId = menuItem.ItemId;
            
            switch (viewId)
            {
                case Resource.Id.shift_expenses:
                    ViewModel.ShowCurrentShiftExpensesCommand.Execute(null);
                    break;
                case Resource.Id.shift_sales:
                    ViewModel.ShowCurrentSalesCommand.Execute(null);
                    break;
                case Resource.Id.end_shift:
                    ViewModel.EndShiftCommand.Execute(null);
                    break;
                case Resource.Id.add_expense:
                    ViewModel.ShowExpenseCommand.Execute(null);
                    break;
                case Resource.Id.inventory:
                    ViewModel.ShowInventoryCommand.Execute(null);
                    break;
                case Resource.Id.utilize_suply_product:
                    ViewModel.ShowUtilizeCommand.Execute(null);
                    break;
                case Resource.Id.undo_shift:
                    ViewModel.DiscardShiftCommand.Execute(null);
                    break;
//                case Resource.Id.motivation:
//                    ViewModel.ShowMotivationCommand.Execute(null);
//                    break;
                case Resource.Id.settings:
                    ViewModel.ShowSettingsCommand.Execute(null);
                    break;
                default:
                    break;
            }
            drawerLayout.CloseDrawer(GravityCompat.Start);
            
            return false;
        }

        #endregion
    }
}
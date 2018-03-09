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
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class MainView : ActivityBase<MainViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private readonly int drawerGravity = GravityCompat.Start;

        private DrawerLayout drawerLayout;
        private ViewPager viewPager;
        private View policeSaveView;
        private View creditCardView;
        private ImageView policeButton;
        private ImageView creditCardButton;
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
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);

            InitLeftMenu();
            viewPager = FindViewById<ViewPager>(Resource.Id.main_viewpager);
            InitToolBarCommands();
            DoBind();
        }

      
        
        private void DoBind()
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
            SupportActionBar.SetCustomView(Resource.Layout.action_bar);
            SupportActionBar.SetDisplayShowCustomEnabled(true);

            policeSaveView = FindViewById<View>(Resource.Id.police_sale_enabled);

            policeButton = FindViewById<ImageView>(Resource.Id.police_sale);
            policeButton.Click += PoliceSale_Click;

            creditCardView = FindViewById<View>(Resource.Id.credit_card_enabled);

            creditCardButton = FindViewById<ImageView>(Resource.Id.credit_card);
            creditCardButton.Click += CreditCard_Click;
        }

        private void CreditCard_Click(object sender, EventArgs e)
        {
            ViewModel.EnableCreditCardSaleCommand.Execute(null);
            creditCardView.Visibility = ViewModel.IsCreditCardSaleEnabled ? ViewStates.Visible : ViewStates.Invisible;
        }

        private void PoliceSale_Click(object sender, System.EventArgs e)
        {
            ViewModel.EnablePoliceSaleCommand.Execute(null);
            policeSaveView.Visibility = ViewModel.IsPoliceSaleEnabled ? ViewStates.Visible : ViewStates.Invisible;
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

        public override void Finish()
        {
            policeButton.Click -= PoliceSale_Click;
            creditCardButton.Click -= CreditCard_Click;

            base.Finish();

        }

        #region DrawerLayout

        private void InitLeftMenu()
        {
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.main_drawer);
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
            if (itemId == Android.Resource.Id.Home)
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

        private void OpenDrawer()
        {
            drawerLayout.OpenDrawer(drawerGravity);
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
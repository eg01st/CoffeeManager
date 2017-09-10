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
using TabItem = CoffeeManager.Droid.Entities.TabItem;
using Android.Support.V4.Widget;
using Android.Support.V7.App;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainView : ActivityBase<MainViewModel>
    {
        private readonly int drawerGravity = GravityCompat.Start;

        private DrawerLayout drawerLayout;
        private ViewPager viewPager;
        private TabLayout tabLayout;
        private View _policeSaveView;
        private View _creditCardView;

        private CoffeeFragment coffeeFragment;
        private TeaFragment teaFragment;
        private SweetsFragment sweetsFragment;
        private WaterFragment waterFragment;
        private AddsFragment addsFragment;
        private MealsFragment mealsFragment;
        private ColdDrinksFragment coldDrinksFragment;
        private IceCreamFragment iceCreamFragment;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);

            viewPager = FindViewById<ViewPager>(Resource.Id.main_viewpager);
            tabLayout = FindViewById<TabLayout>(Resource.Id.main_tabs);

            InitToolBarCommands();
            SetTabLayout();

            InitLeftMenu();

            DoBind();
        }

        private void InitLeftMenu()
        {
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.main_drawer);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, Resource.Drawable.ic_menu_black_24dp, Resource.Drawable.ic_keyboard_backspace_black_24dp);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            var ids = GetMenuItemIds();
            foreach (var id in ids)
            {
                var view = FindViewById(id);
                view.SetOnClickListener(OnMenuitemClicked);
            }
        }

        private int[] GetMenuItemIds()
        {
            return
                new[]
                {
                    Resource.Id.add_expense,
                    Resource.Id.shift_expenses,
                    Resource.Id.shift_sales,
                    Resource.Id.end_shift,
                    Resource.Id.inventory
                };
        }

        private void OnMenuitemClicked(View view)
        {
            OnMenuItemClicked(view.Id);

            drawerLayout.CloseDrawers();
        }

        private void OnMenuItemClicked(int viewId)
        {
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
                default:
                    break;
            }
        }

        private void DoBind()
        {
            var set = this.CreateBindingSet<MainView, MainViewModel>();

            set.Bind(coffeeFragment).For(v => v.ViewModel).To(vm => vm.CoffeeProducts).OneWay();
            set.Bind(teaFragment).For(v => v.ViewModel).To(vm => vm.TeaProducts).OneWay();
            set.Bind(sweetsFragment).For(v => v.ViewModel).To(vm => vm.SweetsProducts).OneWay();
            set.Bind(waterFragment).For(v => v.ViewModel).To(vm => vm.WaterProducts).OneWay();
            set.Bind(addsFragment).For(v => v.ViewModel).To(vm => vm.AddsProducts).OneWay();
            set.Bind(mealsFragment).For(v => v.ViewModel).To(vm => vm.MealsProducts).OneWay();
            set.Bind(coldDrinksFragment).For(v => v.ViewModel).To(vm => vm.ColdDrinksProducts).OneWay();
            set.Bind(iceCreamFragment).For(v => v.ViewModel).To(vm => vm.IceCreamProducts).OneWay();
            set.Apply();
        }

        private void InitToolBarCommands()
        {
            SupportActionBar.SetCustomView(Resource.Layout.action_bar);
            SupportActionBar.SetDisplayShowCustomEnabled(true);

            _policeSaveView = FindViewById<View>(Resource.Id.police_sale_enabled);

            var police = FindViewById<ImageView>(Resource.Id.police_sale);
            police.Click += PoliceSale_Click;

            _creditCardView = FindViewById<View>(Resource.Id.credit_card_enabled);

            var creditCard = FindViewById<ImageView>(Resource.Id.credit_card);
            creditCard.Click += CreditCard_Click;
        }

        private void CreditCard_Click(object sender, EventArgs e)
        {
            ViewModel.EnableCreditCardSaleCommand.Execute(null);
            _creditCardView.Visibility = ViewModel.IsCreditCardSaleEnabled ? ViewStates.Visible : ViewStates.Invisible;
        }

        private void PoliceSale_Click(object sender, System.EventArgs e)
        {
            ViewModel.EnablePoliceSaleCommand.Execute(null);
            _policeSaveView.Visibility = ViewModel.IsPoliceSaleEnabled ? ViewStates.Visible : ViewStates.Invisible;
        }

        private void SetTabLayout()
        {
            var tabItems = ProduceTabItems();
            SetupViewPager(tabItems);
            tabLayout.SetupWithViewPager(viewPager);

            for (var i = 0; i < tabItems.Length; i++)
            {
                var tab = tabLayout.GetTabAt(i);
                var tabItem = tabItems[i];
                var view = new LinearLayout(this.BaseContext);
                view.SetGravity(GravityFlags.Center);
                var param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                param.Gravity = GravityFlags.Left;
                view.LayoutParameters = param;
                var textView = new TextView(this.BaseContext);
                textView.TextSize = 20;
                textView.SetTextColor(Android.Graphics.Color.Black);
                textView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent); ;
                textView.Text = tabItem.Title;
                view.AddView(textView);
                tab.SetCustomView(view);
            }
        }

        private void SetupViewPager(IEnumerable<TabItem> tabItems)
        {
            var adapter = new ViewPagerAdapter(SupportFragmentManager);
            foreach (var tabItem in tabItems)
            {
                var fragment = tabItem.Fragment;
                var title = tabItem.Title;
                adapter.AddFragment(fragment, title);
            }

            viewPager.Adapter = adapter;
        }

        private TabItem[] ProduceTabItems()
        {
            coffeeFragment = new CoffeeFragment();
	        teaFragment = new TeaFragment();
	        sweetsFragment = new SweetsFragment();
	        waterFragment = new WaterFragment();
	        addsFragment = new AddsFragment();
	        mealsFragment = new MealsFragment();
	        coldDrinksFragment= new ColdDrinksFragment();
	        iceCreamFragment = new IceCreamFragment();

            return new TabItem[]
            {
                new TabItem("Кофе", coffeeFragment),
                new TabItem("Чай", teaFragment),
                new TabItem("Сладости", sweetsFragment),
                new TabItem("Вода", waterFragment),
                new TabItem("Добавки", addsFragment),
                new TabItem("Еда", mealsFragment),
                new TabItem("Хол напитки", coldDrinksFragment),
                new TabItem("Мороженое", iceCreamFragment),
            };
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

    }
}
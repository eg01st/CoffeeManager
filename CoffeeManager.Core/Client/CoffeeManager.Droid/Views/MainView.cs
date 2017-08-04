using System;
using System.Collections.Generic;
using System.Net.Mail;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Adapters;
using CoffeeManager.Droid.Views.Fragments;
using MvvmCross.Binding.BindingContext;
using TabItem = CoffeeManager.Droid.Entities.TabItem;

namespace CoffeeManager.Droid.Views
{
    [Activity(Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainView : ActivityBase<MainViewModel>
    {
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

            DoBind();
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

            var endShiftImage = FindViewById<ImageView>(Resource.Id.end_shift_icon);
            endShiftImage.Click += Image_Click;

            var currentSales = FindViewById<TextView>(Resource.Id.current_shift_sales);
            currentSales.Click += CurrentSales_Click;


            var expense = FindViewById<TextView>(Resource.Id.exprense);
            expense.Click += Expense_Click;


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

        private void Expense_Click(object sender, System.EventArgs e)
        {
            ViewModel.ShowExpenseCommand.Execute(null);
        }

        private void CurrentSales_Click(object sender, System.EventArgs e)
        {
            ViewModel.ShowCurrentSalesCommand.Execute(null);
        }


        private void Image_Click(object sender, System.EventArgs e)
        {
            ViewModel.EndShiftCommand.Execute(null);
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
                tab.SetText(tabItem.Title);
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

        }
    }
}
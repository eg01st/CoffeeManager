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

        private TabFactory tabFactory = new TabFactory();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);

            viewPager = FindViewById<ViewPager>(Resource.Id.main_viewpager);
            tabLayout = FindViewById<TabLayout>(Resource.Id.main_tabs);

            InitToolBarCommands();
            SetTabLayout();
        }

        private void InitToolBarCommands()
        {
            SupportActionBar.SetCustomView(Resource.Layout.action_bar);
            SupportActionBar.SetDisplayShowCustomEnabled(true);

            var endShiftImage = FindViewById<ImageView>(Resource.Id.end_shift_icon);
            endShiftImage.Click += Image_Click;

            var deptsImage = FindViewById<TextView>(Resource.Id.dept_icon);
            deptsImage.Click += DeptsImage_Click;

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

        private void DeptsImage_Click(object sender, System.EventArgs e)
        {
            ViewModel.ShowDeptsCommand.Execute(null);
        }


        private void Image_Click(object sender, System.EventArgs e)
        {
            ViewModel.EndShiftCommand.Execute(null);
        }

        private void SetTabLayout()
        {
            var tabItems = tabFactory.Produce();
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

        public override void OnBackPressed()
        {

        }
    }
}
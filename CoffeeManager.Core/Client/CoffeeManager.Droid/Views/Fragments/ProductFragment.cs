using Android.OS;
using Android.Views;
using CoffeManager.Common;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using CoffeeManager.Droid.Adapters;
using CoffeeManager.Core.ViewModels.Products;
using MvvmCross.Binding.BindingContext;
using System;

namespace CoffeeManager.Droid.Views.Fragments
{
    public class ProductFragment : MvxFragment
    {
        private TabLayout tabLayout;
        private ViewPager viewPager;

        private ProductViewModel ViewModel => base.ViewModel as ProductViewModel;

        public bool HasSubCategories
        {
            get => ViewModel?.HasSubCategories ?? false;
            set
            {
                if(value)
                {
                    SetupViewPager();
                }
            }
        }

        private void SetupViewPager()
        {
            var adapter = new ViewPagerAdapter(FragmentManager);

            foreach (var category in ViewModel.SubCategories)
            {
                //var vm = ViewModel.Products.First(p => p.CategoryId == category.Id);
                var fragment = new ProductFragment();
                fragment.DataContext = category;
                adapter.AddFragment(fragment, category.CategoryName);
            }

            viewPager.Adapter = adapter;
            viewPager.OffscreenPageLimit = ViewModel.SubCategories.Count;

            tabLayout.SetupWithViewPager(viewPager);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(inflater);
            var view = this.BindingInflate(Resource.Layout.product_list, null);

            tabLayout = view.FindViewById<TabLayout>(Resource.Id.sub_category_tab_layout);
            viewPager = view.FindViewById<ViewPager>(Resource.Id.sub_category_viewpager);

            DoBind();


            return view;
        }

        private void DoBind()
        {
            var set = this.CreateBindingSet<ProductFragment, ProductViewModel>();
            set.Bind(this).For(t => t.HasSubCategories).To(vm => vm.HasSubCategories);
            set.Apply();
        }
    }
}
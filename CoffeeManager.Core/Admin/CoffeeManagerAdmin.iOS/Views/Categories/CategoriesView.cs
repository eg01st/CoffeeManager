using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    public partial class CategoriesView : ViewControllerBase<CategoriesViewModel>
    {
        private SimpleTableSource datasource;

        public CategoriesView() : base("CategoriesView", null)
        {
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            var addCategoryButton = new UIBarButtonItem()
            {
                Image = UIImage.FromBundle("ic_add_circle_outline")
            };

            NavigationItem.SetRightBarButtonItem(addCategoryButton, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {addCategoryButton, "Clicked AddCategoryCommand"},
            });
        }

        protected override void InitStylesAndContent()
        {
            Title = "Категории товаров";
            datasource = new SimpleTableSource(CategoriesTableView, CategoryTableViewCell.Key, CategoryTableViewCell.Nib);
            CategoriesTableView.Source = datasource;
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<CategoriesView, CategoriesViewModel>();
            set.Bind(datasource).To(vm => vm.ItemsCollection);
            set.Bind(datasource).For(d => d.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();
        }
    }
}


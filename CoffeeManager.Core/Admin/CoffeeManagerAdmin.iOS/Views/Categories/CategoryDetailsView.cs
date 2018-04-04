using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    public partial class CategoryDetailsView : ViewControllerBase<CategoryDetailsViewModel>
    {
        private SimpleTableSource dataSource;
        
        public CategoryDetailsView() : base("CategoryDetailsView", null)
        {
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            
            var saveButton = new UIBarButtonItem()
            {
                Title = "Сохранить"
            };

            NavigationItem.SetRightBarButtonItem(saveButton, true);
            this.AddBindings(new Dictionary<object, string>
            {
                {saveButton, "Clicked SaveChangesCommand"},
            });
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            Title = "Детали категории";
            
            dataSource = new SimpleTableSource(SubCategoriesTableView,
                CategoryTableViewCell.Key, CategoryTableViewCell.Nib);
            SubCategoriesTableView.Source = dataSource;

        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<CategoryDetailsView, CategoryDetailsViewModel>();
            set.Bind(NameTextField).To(vm => vm.Name);
            set.Bind(AddSubCategoryButton).To(vm => vm.AddSubCategoryCommand);
            set.Bind(dataSource).To(vm => vm.SubCategories);
            set.Apply();
        }
    }
}


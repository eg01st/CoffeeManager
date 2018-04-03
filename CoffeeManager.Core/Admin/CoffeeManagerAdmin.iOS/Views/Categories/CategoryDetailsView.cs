using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    public partial class CategoryDetailsView : ViewControllerBase<CategoryDetailsViewModel>
    {
        private SimpleTableSource dataSource;
        
        public CategoryDetailsView() : base("CategoryDetailsView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            
            dataSource = new SimpleTableSource(SubCategoriesTableView,
                CategoryTableViewCell.Key, CategoryTableViewCell.Nib);
            
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


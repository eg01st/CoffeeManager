using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Categories
{
    public partial class AddCategoryView : ViewControllerBase<AddCategoryViewModel>
    {
        public AddCategoryView() : base("AddCategoryView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            Title = "Добавить категорию";
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<AddCategoryView, AddCategoryViewModel>();
            set.Bind(NameTextFiled).To(vm => vm.CategoryName);
            set.Bind(AddButton).To(vm => vm.AddCategoryCommand);
            set.Apply();
        }
    }
}


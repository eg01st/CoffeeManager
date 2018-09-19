using CoffeeManagerAdmin.Core.ViewModels.Products;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS.Views.Products
{
    public partial class AddProductView : ViewControllerBase<AddProductViewModel>
    {
        public AddProductView() : base("AddProductView", null)
        {
        }

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();
            Title = "Добавить товар";
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<AddProductView, AddProductViewModel>();
            set.Bind(AddButton).To(vm => vm.AddProductCommad);
            set.Bind(ProductNameTextField).To(vm => vm.ProductName);
            set.Apply();
        }
    }
}


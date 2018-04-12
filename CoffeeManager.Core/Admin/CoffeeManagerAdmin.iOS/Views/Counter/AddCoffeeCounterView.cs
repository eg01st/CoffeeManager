using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    public partial class AddCoffeeCounterView : ViewControllerBase<AddCoffeeCounterViewModel>
    {
        public AddCoffeeCounterView() : base("AddCoffeeCounterView", null)
        {
        }


        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            navigationItem.Title = "Добавить счетчик";
            
            var btn = new UIBarButtonItem();
            btn.Title = "Добавить";
            
            navigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddCounterCommand"},
            });  
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<AddCoffeeCounterView, AddCoffeeCounterViewModel>();
            set.Bind(NameTextField).To(vm => vm.CounterName);
            set.Bind(CategoryTextField).To(vm => vm.CategoryName);
            set.Bind(SuplyProductTextField).To(vm => vm.SuplyProductName);
            set.Bind(SelectCategoryButton).To(vm => vm.SelectCategoryCommand);
            set.Bind(SelectSuplyProductButton).To(vm => vm.SelectSuplyProductCommand);
            set.Apply();
        }
    }
}


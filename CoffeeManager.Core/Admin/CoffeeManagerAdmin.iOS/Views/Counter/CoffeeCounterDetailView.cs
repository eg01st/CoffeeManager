﻿using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Counter
{
    public partial class CoffeeCounterDetailView : ViewControllerBase<CoffeeCounterDetailViewModel>
    {
        public CoffeeCounterDetailView() : base("AddCoffeeCounterView", null)
        {
        }


        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);

            navigationItem.Title = "Измеить счетчик";
            
            var btn = new UIBarButtonItem();
            btn.Title = "Сохранить";
            
            navigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked AddCounterCommand"},
            });  
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<CoffeeCounterDetailView, CoffeeCounterDetailViewModel>();
            set.Bind(NameTextField).To(vm => vm.CounterName);
            set.Bind(CategoryTextField).To(vm => vm.CategoryName);
            set.Bind(SuplyProductTextField).To(vm => vm.SuplyProductName);
            set.Bind(SelectCategoryButton).To(vm => vm.SelectCategoryCommand);
            set.Bind(SelectSuplyProductButton).To(vm => vm.SelectSuplyProductCommand);
            set.Apply();
        }
    }
}


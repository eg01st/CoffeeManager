using System.Collections.Generic;
using CoffeeManagerAdmin.Core.ViewModels.Motivation;
using CoffeeManagerAdmin.iOS.TableSources;
using MobileCore.iOS.ViewControllers;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Views.Motivation
{
    public partial class MotivationView : ViewControllerBase<MotivationViewModel>
    {
        private SimpleTableSource source;

        public MotivationView() : base("MotivationView", null)
        {
        }

        protected override void InitNavigationItem(UINavigationItem navigationItem)
        {
            base.InitNavigationItem(navigationItem);
            var btn = new UIBarButtonItem()
            {
                Title = "Новая мотивация"
            };
            navigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked StartMotivationCommand"},

            });
        }

        protected override void DoViewDidLoad()
        {
            source = new SimpleTableSource(MotivationTableView,
                                               MotivationViewCell.Key,
                                               MotivationViewCell.Nib,
                                               MotivationTableViewHeader.Key,
                                               MotivationTableViewHeader.Nib);
            MotivationTableView.Source = source;
        }

        protected override void DoBind()
        {
            base.DoBind();

            var set = this.CreateBindingSet<MotivationView, MotivationViewModel>();
            set.Bind(source).To(vm => vm.ItemsCollection);
            set.Bind(FinishMotivationButton).To(vm => vm.FinishMotivationCommand);
            set.Bind(MotivationStartDateLabel).To(vm => vm.MotivationStartDate);
            set.Apply();
        }
    }
}


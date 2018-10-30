using System;

using UIKit;
using MobileCore.iOS.ViewControllers;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.Extensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using CoffeeManagerAdmin.iOS.TableSources;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddAutoOrderView : ViewControllerBase<AddAutoOrderViewModel>
    {
        private MvxPickerViewModel dayOfWeekPickerViewModel;
        private MvxPickerViewModel hourPickerViewModel;
        private SimpleTableSource tableSource;

        public AddAutoOrderView() : base("AddAutoOrderView", null)
        {
        }


        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            var toolbar = Helper.ProducePickerToolbar(View);

            dayOfWeekPickerViewModel = Helper.ProducePicker(WeekDayTextField, toolbar);
            hourPickerViewModel = Helper.ProducePicker(HourTextField, toolbar);


            tableSource = new SimpleTableSource(SUplyProductsTableView, SuplyProductToOrderItemViewCell.Key, SuplyProductToOrderItemViewCell.Nib);
            SUplyProductsTableView.Source = tableSource;
        }

        protected override void DoBind()
        {
       
            base.DoBind();
            var set = this.CreateBindingSet<AddAutoOrderView, AddAutoOrderViewModel>();
            set.Bind(dayOfWeekPickerViewModel).For(p => p.ItemsSource).To(vm => vm.DaysOfWeek);
            set.Bind(dayOfWeekPickerViewModel).For(p => p.SelectedItem).To(vm => vm.DayOfWeek);
            set.Bind(WeekDayTextField).To(vm => vm.DayOfWeek);

            set.Bind(hourPickerViewModel).For(p => p.ItemsSource).To(vm => vm.Hours);
            set.Bind(hourPickerViewModel).For(p => p.SelectedItem).To(vm => vm.OrderTime);
            set.Bind(HourTextField).To(vm => vm.OrderTime);

            set.Bind(tableSource).For(p => p.ItemsSource).To(vm => vm.ItemsCollection);
            set.Bind(tableSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);

            set.Bind(AddSuplyProductsButton).To(vm => vm.AddSuplyProductsCommand);


            set.Apply();
        }
    }
}


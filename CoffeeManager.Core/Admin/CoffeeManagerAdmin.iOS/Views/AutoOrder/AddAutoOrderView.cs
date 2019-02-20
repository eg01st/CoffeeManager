using System;

using UIKit;
using MobileCore.iOS.ViewControllers;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.Extensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using CoffeeManagerAdmin.iOS.TableSources;
using System.Collections.Generic;
using CoffeeManagerAdmin.iOS.Views.AutoOrder;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AddAutoOrderView : KeyboardViewControllerBase<AddAutoOrderViewModel>
    {
        private MvxPickerViewModel dayOfWeekPickerViewModel;
        private MvxPickerViewModel hourPickerViewModel;
        private SimpleTableSource tableSource;

        public AddAutoOrderView() : base("AddAutoOrderView", null)
        {
        }

        protected override bool HandlesKeyboardNotifications => true;

        protected override void InitStylesAndContent()
        {
            base.InitStylesAndContent();

            DismissKeyboardOnBackgroundTap();

            var toolbar = Helper.ProducePickerToolbar(View);

            dayOfWeekPickerViewModel = Helper.ProducePicker(OrderWeekDayTextField, toolbar);
            hourPickerViewModel = Helper.ProducePicker(OrderTimeTextField, toolbar);


            tableSource = new SimpleTableSource(SUplyProductsTableView, SuplyProductToOrderItemViewCell.Key, SuplyProductToOrderItemViewCell.Nib);
            SUplyProductsTableView.Source = tableSource;

            Title = "Новый автозаказ";

            var btn = new UIBarButtonItem()
            {
                Title = "Сохранить"
            };
            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked SaveAutoOrderCommand"},
            });
        }

        protected override void DoBind()
        {
       
            base.DoBind();
            var set = this.CreateBindingSet<AddAutoOrderView, AddAutoOrderViewModel>();
            set.Bind(dayOfWeekPickerViewModel).For(p => p.ItemsSource).To(vm => vm.DaysOfWeek);
            set.Bind(dayOfWeekPickerViewModel).For(p => p.SelectedItem).To(vm => vm.DayOfWeek);
            set.Bind(OrderWeekDayTextField).To(vm => vm.DayOfWeek);

            set.Bind(hourPickerViewModel).For(p => p.ItemsSource).To(vm => vm.Hours);
            set.Bind(hourPickerViewModel).For(p => p.SelectedItem).To(vm => vm.OrderTime);
            set.Bind(OrderTimeTextField).To(vm => vm.OrderTime);

            set.Bind(EmailTextField).To(vm => vm.EmailToSend);
            set.Bind(CCTextField).To(vm => vm.CCToSend);
            set.Bind(SenderEmailTextField).To(vm => vm.SenderEmail);
            set.Bind(PasswordTextField).To(vm => vm.SenderEmailPassword);
            set.Bind(SubjectTextField).To(vm => vm.Subject);

            set.Bind(tableSource).For(p => p.ItemsSource).To(vm => vm.ItemsCollection);
            set.Bind(tableSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);

            set.Bind(AddSuplyProductsButton).To(vm => vm.AddSuplyProductsCommand);


            set.Apply();
        }
    }
}


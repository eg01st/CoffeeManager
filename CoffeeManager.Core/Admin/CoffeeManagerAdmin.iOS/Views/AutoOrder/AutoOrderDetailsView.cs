using System;

using UIKit;
using MobileCore.iOS.ViewControllers;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;
using CoffeeManagerAdmin.iOS.TableSources;
using System.Collections.Generic;
using CoffeeManagerAdmin.iOS.Views.AutoOrder;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using CoffeeManagerAdmin.iOS.Extensions;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AutoOrderDetailsView : KeyboardViewControllerBase<AutoOrderDetailsViewModel>
    {
        private MvxPickerViewModel dayOfWeekPickerViewModel;
        private MvxPickerViewModel hourPickerViewModel;
        private SimpleTableSource tableSource;

        public AutoOrderDetailsView() : base("AutoOrderDetailsView", null)
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


            tableSource = new SimpleTableSource(OrderItemsTableView, SuplyProductToOrderItemViewCell.Key, SuplyProductToOrderItemViewCell.Nib);
            OrderItemsTableView.Source = tableSource;

            Title = "Детали автозаказа";

            var btn = new UIBarButtonItem()
            {
                Title = "Сохранить"
            };
            NavigationItem.SetRightBarButtonItem(btn, false);
            this.AddBindings(new Dictionary<object, string>
            {
                {btn, "Clicked UpdateOrderCommand"},
            });
        }

        protected override void DoBind()
        {
            base.DoBind();
            var set = this.CreateBindingSet<AutoOrderDetailsView, AutoOrderDetailsViewModel>();
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
            set.Bind(tableSource).For(p => p.LongPressCommand).To(vm => vm.ItemSelectedCommand);
            set.Bind(AddSuplyProductsButton).To(vm => vm.AddSuplyProductsCommand);

            set.Apply();
        }
    }
}


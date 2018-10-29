using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AddAutoOrderViewModel : FeedViewModel<SuplyProductToOrderItemViewModel>, IMvxViewModelResult<bool>
    {
        private DayOfWeek dayOfWeek;
        private TimeSpan orderTime;
        
        private readonly IAutoOrderManager manager;
        public ICommand AddSuplyProductsCommand { get; }
        
        public ICommand SaveAutoOrderCommand { get; }
        
        public DayOfWeek DayOfWeek
        {
            get => dayOfWeek;
            set => SetProperty(ref dayOfWeek, value);
        }
        public TimeSpan OrderTime
        {
            get => orderTime;
            set => SetProperty(ref orderTime, value);
        }

        public AddAutoOrderViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
            AddSuplyProductsCommand = new MvxAsyncCommand(DoAddSuplyProducts);
            SaveAutoOrderCommand = new MvxAsyncCommand(DoSaveAutoOrder, () => ItemsCollection.Count > 0);
        }

        private async Task DoSaveAutoOrder()
        {
            var order = new AutoOrderDTO();
            order.DayOfWeek = dayOfWeek;
            order.OrderTime = orderTime;
            order.IsActive = true;
            order.OrderItems = ItemsCollection.Select(MapItem).ToList();

            await ExecuteSafe(manager.AddAutoOrderItem(order));
            await NavigationService.Close(this, true);
        }

        private SuplyProductToOrderItemDTO MapItem(SuplyProductToOrderItemViewModel vm)
        {
            return new SuplyProductToOrderItemDTO()
            {
                SuplyProductId = vm.SuplyProductId,
                QuantityShouldBeAfterOrder = vm.QuantityShouldBeAfterOrder
            };
        }

        private async Task DoAddSuplyProducts()
        {
            var suplyProducts = await NavigationService.Navigate<SelectSuplyProductsViewModel, IEnumerable<SupliedProduct>>();
            ItemsCollection.AddRange(suplyProducts.Select(s => new SuplyProductToOrderItemViewModel(s.Id, s.Name)));
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }
}
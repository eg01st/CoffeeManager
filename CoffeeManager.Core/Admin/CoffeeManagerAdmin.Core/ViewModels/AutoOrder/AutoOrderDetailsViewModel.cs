using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AutoOrderDetailsViewModel : FeedViewModel<SuplyProductToOrderItemViewModel> , IMvxViewModel<int, bool>
    {
        private readonly IAutoOrderManager manager;
        private int autoOrderId;
        
        public ICommand DeleteOrderCommand { get; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OrderTime { get; set; }
        public bool IsActive { get; set; }

        public AutoOrderDetailsViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
            DeleteOrderCommand = new MvxAsyncCommand(DoDeleteOrder);
        }

        protected override async Task DoClose()
        {
            await NavigationService.Close(this, false);
        }

        private async Task DoDeleteOrder()
        {
            Confirm("Удалить автозаказ?", async () =>
             {
                 await ExecuteSafe(manager.DeleteAutoOrderItem(autoOrderId));
                 await NavigationService.Close(this, true);
             });
        }

        public void Prepare(int parameter)
        {
            autoOrderId = parameter;
        }

        protected override async Task<PageContainer<SuplyProductToOrderItemViewModel>> GetPageAsync(int skip)
        {
            var order = await ExecuteSafe(async () => await manager.GetAutoOrderDetails(autoOrderId));
            DayOfWeek = order.DayOfWeek;
            OrderTime = order.OrderTime;
            IsActive = order.IsActive;
            
            return order.OrderItems.Select(MapItem).ToPageContainer();
        }

        private SuplyProductToOrderItemViewModel MapItem(SuplyProductToOrderItemDTO dto)
        {
            return new SuplyProductToOrderItemViewModel(dto.SuplyProductId, dto.SuplyProductName, false)
            {
                Id = dto.Id,
                QuantityShouldBeAfterOrder = dto.QuantityShouldBeAfterOrder,
                ShouldUpdateQuantityBeforeOrder = dto.ShouldUpdateQuantityBeforeOrder
            };
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }
}
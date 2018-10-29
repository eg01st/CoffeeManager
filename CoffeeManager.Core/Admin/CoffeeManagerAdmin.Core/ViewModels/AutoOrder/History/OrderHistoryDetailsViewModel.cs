using System;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.AutoOrder.History;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder.History
{
    public class OrderHistoryDetailsViewModel : FeedViewModel<SuplyProductOrderItemViewModel>, IMvxViewModel<int>
    {
        private readonly IAutoOrderManager manager;
        public int OrderId { get; set; }
        
        public DateTime OrderDate { get; set; }

        public OrderHistoryDetailsViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
        }
        
        public void Prepare(int parameter)
        {
            OrderId = parameter;
        }

        protected override async Task<PageContainer<SuplyProductOrderItemViewModel>> GetPageAsync(int skip)
        {
            var item = await ExecuteSafe(async ()=> await manager.GetOrderHistoryDetails(OrderId));
            OrderDate = item.OrderDate;
            return item.OrderedItems.Select(MapItem).ToPageContainer();
        }

        private SuplyProductOrderItemViewModel MapItem(SuplyProductOrderItemDTO dto)
        {
            return new SuplyProductOrderItemViewModel()
            {
                Id = dto.Id,
                SuplyProductName = dto.SuplyProductName,
                SuplyProductId = dto.SuplyProductId,
                OrderedQuantity = dto.OrderedQuantity,
                QuantityBefore = dto.QuantityBefore
            };
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.AutoOrder.History;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder.History
{
    public class OrderHistoryViewModel : FeedViewModel<OrderHistoryItemViewModel>
    {
        private readonly IAutoOrderManager manager;

        public OrderHistoryViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
        }

        protected override async Task<PageContainer<OrderHistoryItemViewModel>> GetPageAsync(int skip)
        {
            var items = await ExecuteSafe(async () => await manager.GetOrdersHistory());
            return items.Select(MapItem).ToPageContainer();
        }

        private OrderHistoryItemViewModel MapItem(OrderHistoryItemDTO dto)
        {
            return new OrderHistoryItemViewModel()
            {
                OrderDate = dto.OrderDate,
                OrderId = dto.OrderId
            };
        }

        protected override async Task OnItemSelectedAsync(OrderHistoryItemViewModel item)
        {
            await NavigationService.Navigate<OrderHistoryDetailsViewModel, int>(item.OrderId);
        }
    }
}
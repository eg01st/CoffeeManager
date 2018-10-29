using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeeManager.Models.Data.DTO.AutoOrder.History;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class AutoOrderManager : IAutoOrderManager
    {
        private readonly IAutoOrderProvider provider;

        public AutoOrderManager(IAutoOrderProvider provider)
        {
            this.provider = provider;
        }
        
        public async Task<IEnumerable<AutoOrderDTO>> GetAutoOrders()
        {
            return await provider.GetAutoOrders();
        }

        public async Task ToggleOrderEnabled(int id)
        {
            await provider.ToggleOrderEnabled(id);
        }

        public async Task<AutoOrderDTO> GetAutoOrderDetails(int id)
        {
            return await provider.GetAutoOrderDetails(id);
        }

        public async Task<int> AddAutoOrderItem(AutoOrderDTO dto)
        {
            return await provider.AddAutoOrderItem(dto);
        }

        public async Task DeleteAutoOrderItem(int id)
        {
            await provider.DeleteAutoOrderItem(id);
        }

        public async Task<IEnumerable<OrderHistoryItemDTO>> GetOrdersHistory()
        {
            return await provider.GetOrdersHistory();
        }

        public async Task<OrderHistoryItemDTO> GetOrderHistoryDetails(int id)
        {
            return await provider.GetOrderHistoryDetails(id);
        }
    }
}
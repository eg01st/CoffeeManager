using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeeManager.Models.Data.DTO.AutoOrder.History;

namespace CoffeManager.Common.Providers
{
    public interface IAutoOrderProvider
    {
        Task<IEnumerable<AutoOrderDTO>> GetAutoOrders();

        Task ToggleOrderEnabled(int id);
        
        Task<AutoOrderDTO> GetAutoOrderDetails(int id);
        
        Task<int> AddAutoOrderItem(AutoOrderDTO dto);
        
        Task DeleteAutoOrderItem(int id);
        
        Task<IEnumerable<OrderHistoryItemDTO>> GetOrdersHistory();
        
        Task<OrderHistoryItemDTO> GetOrderHistoryDetails(int id);
        
        Task UpdateAutoOrderItem(AutoOrderDTO dto);
    }
}
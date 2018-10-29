using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeeManager.Models.Data.DTO.AutoOrder.History;

namespace CoffeManager.Common.Providers
{
    public class AutoOrderProvider : BaseServiceProvider, IAutoOrderProvider
    {
        public async Task<IEnumerable<AutoOrderDTO>> GetAutoOrders()
        {
            return await Get<IEnumerable<AutoOrderDTO>>(RoutesConstants.GetAutoOrders);
        }

        public async Task ToggleOrderEnabled(int id)
        {
            await Post<Object>(RoutesConstants.ToggleProductEnabled, null, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<AutoOrderDTO> GetAutoOrderDetails(int id)
        {
            var dto = await Get<AutoOrderDTO>(RoutesConstants.GetAutoOrderDetails,
                new Dictionary<string, string>()
                {
                    {nameof(id), id.ToString()},
                });
            return dto;
        }

        public async Task<int> AddAutoOrderItem(AutoOrderDTO dto)
        {
            return await Put<int, AutoOrderDTO>(RoutesConstants.AddAutoOrderItem, dto);
        }

        public async Task DeleteAutoOrderItem(int id)
        {
            await Delete(RoutesConstants.DeleteAutoOrderItem, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<IEnumerable<OrderHistoryItemDTO>> GetOrdersHistory()
        {
            return await Get<IEnumerable<OrderHistoryItemDTO>>(RoutesConstants.GetOrdersHistory);
        }

        public async Task<OrderHistoryItemDTO> GetOrderHistoryDetails(int id)
        {
            var dto = await Get<OrderHistoryItemDTO>(RoutesConstants.GetOrderHistoryDetails,
                new Dictionary<string, string>()
                {
                    {nameof(id), id.ToString()},
                });
            return dto;
        }
    }
}
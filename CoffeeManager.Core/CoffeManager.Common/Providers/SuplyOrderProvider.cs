using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class SuplyOrderProvider : BaseServiceProvider, ISuplyOrderProvider
    {
        public async Task<Order[]> GetOrders()
        {
            return await Get<Order[]>(RoutesConstants.GetOrders);
        }

        public async Task<OrderItem[]> GetOrderItems(int id)
        {
            return await Get<OrderItem[]>(RoutesConstants.GetOrderItems, new Dictionary<string, string> { { nameof(id), id.ToString() } });
        }

        public async Task SaveOrderItem(OrderItem item)
        {
            await Post<OrderItem>(RoutesConstants.SaveOrderItem, item);
        }

        public async Task CreateOrderItem(OrderItem item)
        {
            await Put<OrderItem>(RoutesConstants.CreateOrderItem, item);
        }

        public async Task<int> CreateOrder(Order order)
        {
            var res = await Put<Order>(RoutesConstants.CreateOrder, order);
            return int.Parse(res);
        }

        public async Task CloseOrder(Order order)
        {
            await Post<Order>(RoutesConstants.CloseOrder, order);
        }

        public async Task DeleteOrder(int id)
        {
            await Delete(RoutesConstants.DeleteOrder, new Dictionary<string, string> { { nameof(id), id.ToString() } });
        }

        public async Task DeleteOrderItem(int id)
        {
            await Delete(RoutesConstants.DeleteOrderItem, new Dictionary<string, string> { { nameof(id), id.ToString() } });
        }
    }
}

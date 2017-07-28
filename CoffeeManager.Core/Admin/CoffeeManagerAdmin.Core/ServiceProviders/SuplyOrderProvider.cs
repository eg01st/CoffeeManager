using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ServiceProviders
{
    public class SuplyOrderProvider : BaseServiceProvider
    {
        private const string SuplyOrder = "suplyorder";
        public async Task<Order[]> GetOrders()
        {
            return await Get<Order[]>($"{SuplyOrder}/getorders");
        }

        public async Task<OrderItem[]> GetOrderItems(int id)
        {
            return await Get<OrderItem[]>($"{SuplyOrder}/getorderitems", new Dictionary<string, string> { { nameof(id), id.ToString() } });
        }

        public async Task SaveOrderItem(OrderItem item)
        {
            await Post<OrderItem>($"{SuplyOrder}/saveorderitem", item);
        }

        public async Task CreateOrderItem(OrderItem item)
        {
            await Put<OrderItem>($"{SuplyOrder}/createorderitem", item);
        }

        public async Task<int> CreateOrder(Order order)
        {
            var res = await Put<Order>($"{SuplyOrder}/createorder", order);
            return int.Parse(res);
        }

        public async Task CloseOrder(Order order)
        {
            await Post<Order>($"{SuplyOrder}/closeorder", order);
        }

        public async Task DeleteOrder(int id)
        {
            await Delete($"{SuplyOrder}/deleteorder", new Dictionary<string, string> { { nameof(id), id.ToString() } });
        }

        public async Task DeleteOrderItem(int id)
        {
            await Delete($"{SuplyOrder}/deleteorderitem", new Dictionary<string, string> { { nameof(id), id.ToString() } });
        }
    }
}

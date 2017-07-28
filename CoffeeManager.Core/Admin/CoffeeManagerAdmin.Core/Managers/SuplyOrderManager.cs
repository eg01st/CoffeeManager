using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;

namespace CoffeeManagerAdmin.Core.Managers
{
    public class SuplyOrderManager : BaseManager
    {
        private SuplyOrderProvider provider = new SuplyOrderProvider();
        public async Task<Order[]> GetOrders()
        {
            return await provider.GetOrders();
        }

        public async Task<OrderItem[]> GetOrderItems(int id)
        {
            return await provider.GetOrderItems(id);
        }

        public async Task SaveOrderItem(OrderItem item)
        {
            await provider.SaveOrderItem(item);
        }

        public async Task CreateOrderItem(OrderItem item)
        {
            await provider.CreateOrderItem(item);
        }

        public async Task<int> CreateOrder(Order order)
        {
            return await provider.CreateOrder(order);
        }

        public async Task CloseOrder(Order order)
        {
            await provider.CloseOrder(order);
        }

        public async Task DeleteOrder(int id)
        {
            await provider.DeleteOrder(id);
        }

        public async Task DeleteOrderItem(int id)
        {
            await provider.DeleteOrderItem(id);
        }
    }
}

using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface ISuplyOrderProvider
    {
        Task<Order[]> GetOrders();

        Task<OrderItem[]> GetOrderItems(int id);

        Task SaveOrderItem(OrderItem item);

        Task CreateOrderItem(OrderItem item);

        Task<int> CreateOrder(Order order);

        Task CloseOrder(Order order);

        Task DeleteOrder(int id);

        Task DeleteOrderItem(int id);
    }
}

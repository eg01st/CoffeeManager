using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class SuplyOrderProvider : ServiceBase, ISuplyOrderProvider
    {
        public async Task<Order[]> GetOrders()
        {
            var request = CreateGetRequest(RoutesConstants.GetOrders);
            return await ExecuteRequestAsync<Order[]>(request);
        }

        public async Task<OrderItem[]> GetOrderItems(int id)
        {
            var request = CreateGetRequest(RoutesConstants.GetOrderItems);
            request.Parameters.Add(new Parameter(){ Name = nameof(id), Value = id });
            return await ExecuteRequestAsync<OrderItem[]>(request);
        }

        public async Task SaveOrderItem(OrderItem item)
        {
            var request = CreatePostRequest(RoutesConstants.SaveOrderItem);
            request.AddBody(item);
            await ExecuteRequestAsync(request);
        }

        public async Task CreateOrderItem(OrderItem item)
        {
            var request = CreatePutRequest(RoutesConstants.CreateOrderItem);
            request.AddBody(item);
            await ExecuteRequestAsync(request);
        }

        public async Task<int> CreateOrder(Order order)
        {
            var request = CreatePutRequest(RoutesConstants.CreateOrder);
            request.AddBody(order);
            return await ExecuteRequestAsync<int>(request);
        }

        public async Task CloseOrder(Order order)
        {
            var request = CreatePostRequest(RoutesConstants.CloseOrder);
            request.AddBody(order);
            await ExecuteRequestAsync(request);
        }

        public async Task DeleteOrder(int id)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteOrder);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);
        }

        public async Task DeleteOrderItem(int id)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteOrderItem);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);
        }
    }
}

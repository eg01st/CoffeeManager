using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class SuplyOrderController : ApiController
    {
        [Route(RoutesConstants.GetOrders)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrders([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var orders = entities.SuplyOrders.Where(s => s.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
         
            return Request.CreateResponse(HttpStatusCode.OK, orders);            
        }

        [Route(RoutesConstants.GetOrderItems)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrderItems([FromUri]int coffeeroomno,  int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var orders = entities.SuplyOrderItems.Include("SupliedProduct").Where(o => o.SuplyOrderId == id && o.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        [Route(RoutesConstants.SaveOrderItem)]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveOrderItem([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var orderItem = JsonConvert.DeserializeObject<Models.OrderItem>(request);

            var entities = new CoffeeRoomEntities();
            var orderItemDb = entities.SuplyOrderItems.First(o => o.Id == orderItem.Id && o.CoffeeRoomNo == coffeeroomno);
            orderItemDb.Quantity = orderItem.Quantity;
            orderItemDb.Price = orderItem.Price;
            orderItemDb.IsDone = orderItem.IsDone;
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.CreateOrderItem)]
        [HttpPut]
        public async Task<HttpResponseMessage> CreateOrderItem([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var orderItem = JsonConvert.DeserializeObject<Models.OrderItem>(request);

            var entities = new CoffeeRoomEntities();
            var orderItemDb = new SuplyOrderItem
            {
                SuplyProductId = orderItem.SuplyProductId,
                SuplyOrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                IsDone = orderItem.IsDone,
                CoffeeRoomNo = orderItem.CoffeeRoomNo
            };
            entities.SuplyOrderItems.Add(orderItemDb);

            var price = orderItem.Price * orderItem.Quantity;
            var orderDb = entities.SuplyOrders.First(o => o.Id == orderItem.OrderId);
            orderDb.Price += price;

            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.CreateOrder)]
        [HttpPut]
        public async Task<HttpResponseMessage> CreateOrder([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Models.Order>(request);

            var entities = new CoffeeRoomEntities();
            var orderDb = new SuplyOrder
            {
                Date = DateTime.Now,
                Price = order.Price,
                IsDone = order.IsDone,
                ExpenseTypeId = order.ExpenseTypeId,
                CoffeeRoomNo = order.CoffeeRoomNo
            };
            entities.SuplyOrders.Add(orderDb);
            entities.SaveChanges();

            if (order.OrderItems != null)
            {
                entities = new CoffeeRoomEntities();
                foreach (var orderItem in order.OrderItems)
                {
                    var orderItemDb = new SuplyOrderItem()
                    {
                        SuplyOrderId = orderDb.Id,
                        SuplyProductId = orderItem.SuplyProductId,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Price,
                        IsDone = orderItem.IsDone,
                        CoffeeRoomNo = orderItem.CoffeeRoomNo
                    };
                    entities.SuplyOrderItems.Add(orderItemDb);
                }
                await entities.SaveChangesAsync();
            }

            return Request.CreateResponse(HttpStatusCode.OK, orderDb.Id);
        }

        [Route(RoutesConstants.CloseOrder)]
        [HttpPost]
        public async Task<HttpResponseMessage> CloseOrder([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Models.Order>(request);

            var entities = new CoffeeRoomEntities();
            var orderDb = entities.SuplyOrders.First(o => o.Id == order.Id);
            orderDb.IsDone = true;
            orderDb.Price = order.Price;
            orderDb.ExpenseTypeId = order.ExpenseTypeId;

            foreach (var item in orderDb.SuplyOrderItems)
            {
                var quantity = entities.SuplyProductQuantities.FirstOrDefault(s => s.SuplyProductId == item.SuplyProductId && s.CoffeeRoomId == coffeeroomno);
                
                if (quantity != null)
                {
                    quantity.Quantity += item.Quantity;
                }
                else
                {
                    var sp = entities.SupliedProducts.First(s => s.Id == item.SuplyProductId);
                    var newQuantity = new SuplyProductQuantity()
                    {
                        CoffeeRoomId = coffeeroomno,
                        SuplyProductId = item.SuplyProductId.Value,
                        Quantity = item.Quantity * sp.ExpenseNumerationMultyplier
                    };
                    entities.SuplyProductQuantities.Add(newQuantity);
                }  
            }

            var currentShift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno);

            var expense = new Expense
            {
                Amount = order.Price,
                ExpenseType = order.ExpenseTypeId,
                Quantity = 1,
                ShiftId = currentShift.Id,
                CoffeeRoomNo = coffeeroomno
            };
            entities.Expenses.Add(expense);
            currentShift.TotalExprenses += order.Price;
            currentShift.TotalAmount -= order.Price;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.DeleteOrder)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteOrder([FromUri]int coffeeroomno, int id, HttpRequestMessage message)
        {   
            var entities = new CoffeeRoomEntities();
            var orderDb = entities.SuplyOrders.First(o => o.Id == id && o.CoffeeRoomNo == coffeeroomno);
            entities.SuplyOrders.Remove(orderDb);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.DeleteOrderItem)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteOrderItem([FromUri]int coffeeroomno, int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var orderDb = entities.SuplyOrderItems.First(o => o.Id == id && o.CoffeeRoomNo == coffeeroomno);
            entities.SuplyOrderItems.Remove(orderDb);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

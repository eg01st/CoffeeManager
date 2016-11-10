using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class SuplyOrderController : ApiController
    {
        [Route("api/suplyorder/getorders")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrders([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var orders = entities.SuplyOrders.ToList().Select(s => s.ToDTO());
         
            return Request.CreateResponse(HttpStatusCode.OK, orders);            
        }

        [Route("api/suplyorder/getorderitems")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrderItems([FromUri]int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var orders = entities.SuplyOrderItems.Where(o => o.SuplyOrderId == id).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        [Route("api/suplyorder/saveorderitem")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveOrderItem([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var orderItem = JsonConvert.DeserializeObject<Models.OrderItem>(request);

            var entities = new CoffeeRoomEntities();
            var orderItemDb = entities.SuplyOrderItems.First(o => o.Id == orderItem.Id);
            orderItemDb.Quantity = orderItem.Quantity;
            orderItemDb.Price = orderItem.Price;
            orderItemDb.IsDone = orderItem.IsDone;
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyorder/createorderitem")]
        [HttpPut]
        public async Task<HttpResponseMessage> CreateOrderItem([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

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
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyorder/createorder")]
        [HttpPut]
        public async Task<HttpResponseMessage> CreateOrder([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Models.Order>(request);

            var entities = new CoffeeRoomEntities();
            var orderDb = new SuplyOrder
            {
                Date = DateTime.Now,
                Price = order.Price,
                IsDone = order.IsDone,
                CoffeeRoomNo = order.CoffeeRoomNo
            };
            entities.SuplyOrders.Add(orderDb);
            entities.SaveChanges();

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

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyorder/closeorder")]
        [HttpPost]
        public async Task<HttpResponseMessage> CloseOrder([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var request = await message.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Models.Order>(request);

            var entities = new CoffeeRoomEntities();
            var orderDb = entities.SuplyOrders.First(o => o.Id == order.Id);
            orderDb.IsDone = true;
            orderDb.Price = order.Price;

            int metroExpenseId = 6;
            var currentShift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno);

            var expense = new Expense
            {
                Amount = order.Price,
                ExpenseType = metroExpenseId,
                Quantity = 1,
                ShiftId = currentShift.Id
            };
            entities.Expenses.Add(expense);

            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyorder/deleteorder")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteOrder([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }    
            var entities = new CoffeeRoomEntities();
            var orderDb = entities.SuplyOrders.First(o => o.Id == id);

            foreach (var suplyOrderItem in orderDb.SuplyOrderItems)
            {
                entities.SuplyOrderItems.Remove(suplyOrderItem);
            }
            entities.SaveChanges();
            entities.SuplyOrders.Remove(orderDb);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyorder/deleteorderitem")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteOrderItem([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var orderDb = entities.SuplyOrderItems.First(o => o.Id == id);
            entities.SuplyOrderItems.Remove(orderDb);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

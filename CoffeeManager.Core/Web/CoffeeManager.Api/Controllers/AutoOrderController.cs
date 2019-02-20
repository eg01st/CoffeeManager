using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Validation;
using CoffeeManager.Models.Data.DTO.AutoOrder;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class AutoOrderController : ApiController
    {
        [Route(RoutesConstants.GetAutoOrders)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAutoOrders([FromUri]int coffeeroomno)
        {
            var entities = new CoffeeRoomEntities();

            var items = entities.AutoOrders.Where(o => o.CoffeeRoomId == coffeeroomno).ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
        
        [Route(RoutesConstants.ToggleOrderEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleOrderEnabled([FromUri]int coffeeroomno, int id)
        {
            var entities = new CoffeeRoomEntities();
            var order = entities.AutoOrders.FirstOrDefault(t => t.Id == id);
            if (order != null)
            {
                order.IsActive = !order.IsActive;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [Route(RoutesConstants.GetAutoOrderDetails)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAutoOrderDetails([FromUri]int coffeeroomno, int id)
        {
            var entities = new CoffeeRoomEntities();
            var item = entities.AutoOrders.FirstOrDefault(o => o.Id == id);
            if (item != null)
            {
                var dto = item.ToDTO();
                var suplyProductOrderItems = entities.SuplyProductOrderItems
                    .Include(i => i.SupliedProduct)
                    .Where(i => i.OrderId == id)
                    .ToList()
                    .Select(s => s.ToDTO());
                dto.OrderItems = suplyProductOrderItems.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, $"No order with id {id}");
        }
        
        [Route(RoutesConstants.AddAutoOrderItem)]
        [HttpPut]
        public async Task<HttpResponseMessage> AddAutoOrderItem([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync ();
            var dto = JsonConvert.DeserializeObject<AutoOrderDTO> (request);

            var dbEntity = dto.Map();
            var entities = new CoffeeRoomEntities();
            entities.AutoOrders.Add(dbEntity);
            entities.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, dbEntity.Id);
        }
        
        [Route (RoutesConstants.DeleteAutoOrderItem)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAutoOrderItem ([FromUri]int coffeeroomno,  int id)
        {
            var entities = new CoffeeRoomEntities ();
            var order = entities.AutoOrders.FirstOrDefault(p => p.Id == id);
            if(order != null)
            {
                var suplyItemOrders = entities.SuplyProductOrderItems.Where(s => s.OrderId == id);
                entities.SuplyProductOrderItems.RemoveRange(suplyItemOrders);

                var histores = entities.AutoOrdersHistories.Where(o => o.OrderId == id);

                foreach (var history in histores)
                {
                    var items = entities.SuplyProductAutoOrdersHistories.Where(h => h.OrderHistoryId == history.Id);
                    entities.SuplyProductAutoOrdersHistories.RemoveRange(items);
                }
                entities.AutoOrdersHistories.RemoveRange(histores);

                entities.AutoOrders.Remove(order);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"No product with id  {id}");
        }
        
        [Route (RoutesConstants.UpdateAutoOrderItem)]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateAutoOrderItem ([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync ();
            var dto = JsonConvert.DeserializeObject<AutoOrderDTO> (request);
            
            var entities = new CoffeeRoomEntities ();
            var order = entities.AutoOrders.FirstOrDefault(p => p.Id == dto.Id);
            if(order != null)
            {
                DbMapper.Update(dto, order);
                entities.SaveChanges();
                var suplyItemOrders = entities.SuplyProductOrderItems.Where(s => s.OrderId == dto.Id);
               
                //Remove deleted items, update current
                foreach (var dbItem in suplyItemOrders)
                {
                    var item = dto.OrderItems.FirstOrDefault(i => i.Id == dbItem.Id);

                    try
                    {
                        if (item != null)
                        {
                            dbItem.QuantityShouldBeAfterOrder = item.QuantityShouldBeAfterOrder;
                            dbItem.ShouldUpdateQuantityBeforeOrder = item.ShouldUpdateQuantityBeforeOrder;
                            continue;
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        string mess = null;
                        foreach (var er in e.EntityValidationErrors)
                        {
                            var m = string.Join(";", er.ValidationErrors.Select(v => $"{v.PropertyName} {v.ErrorMessage}"));
                            mess += m;
                        }

                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mess);
                    }
                
                    entities.SuplyProductOrderItems.Remove(dbItem);
                }
                entities.SaveChanges();

                //Add new items
                foreach (var item in dto.OrderItems.Where(o => o.Id == 0))
                {
                    var dbItem = item.Map();
                    entities.SuplyProductOrderItems.Add(dbItem);
                    entities.SaveChanges();
                }

                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
        }
        
        [Route(RoutesConstants.GetOrdersHistory)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrdersHistory([FromUri]int coffeeroomno)
        {
            var entities = new CoffeeRoomEntities();
            var items = entities.AutoOrdersHistories.Where(o => o.CoffeeRoomId == coffeeroomno).ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
        
                
        [Route(RoutesConstants.GetOrderHistoryDetails)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrderHistoryDetails([FromUri]int coffeeroomno, int id)
        {
            var entities = new CoffeeRoomEntities();
            var item = entities.AutoOrdersHistories.FirstOrDefault(o => o.Id == id);
            if (item != null)
            {
                var dto = item.ToDTO();
                var suplyProductOrderItems = entities.SuplyProductAutoOrdersHistories
                    .Include(i => i.SupliedProduct)
                    .Where(i => i.OrderHistoryId == id)
                    .ToList()
                    .Select(s => s.ToDTO());
                dto.OrderedItems = suplyProductOrderItems.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, $"No order with id {id}");
        }
    }
}
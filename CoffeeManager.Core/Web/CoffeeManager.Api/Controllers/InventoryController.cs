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
using CoffeeManager.Models.Data.DTO.AutoOrder;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        [Route(RoutesConstants.GetInventoryItems)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetInventoryItems([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var items = entities.SupliedProducts.Include(p => p.SuplyProductQuantities).Where(s => s.InventoryEnabled).ToList().Select(s => s.ToDTO(coffeeroomno));

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [Route(RoutesConstants.SentInventoryInfo)]
        [HttpPost]
        public async Task<HttpResponseMessage> SentInventoryInfo([FromUri]int coffeeroomno, HttpRequestMessage message)
        {

            var request = await message.Content.ReadAsStringAsync();
            var inventoryItems = JsonConvert.DeserializeObject<InventoryItem[]>(request);
            if(inventoryItems.Length < 1)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty inventory items");
            }
            var entities = new CoffeeRoomEntities();
            var report = new InventoryReport();
            report.CoffeeRoomNo = coffeeroomno;
            report.Date = DateTime.Now;
            report.InventoryReportItems = new List<InventoryReportItem>();

            foreach (var inv in inventoryItems)
            {
                var inventoryItem = new InventoryReportItem();
                inventoryItem.CoffeeRoomNo = coffeeroomno;
                inventoryItem.SuplyProductId = inv.SuplyProductId;
                inventoryItem.QuantityBefore = inv.QuantityBefore;
                inventoryItem.QuantityAfter = inv.QuantityAfer;
                inventoryItem.QuantityDiff = inv.QuantityAfer - inv.QuantityBefore;
                report.InventoryReportItems.Add(inventoryItem);
            }

            entities.InventoryReports.Add(report);

            await entities.SaveChangesAsync();

            entities = new CoffeeRoomEntities();

            foreach (var inv in inventoryItems)
            {
                var suplyProductQuantity = entities.SuplyProductQuantities.FirstOrDefault(p => p.SuplyProductId == inv.SuplyProductId && p.CoffeeRoomId == coffeeroomno);
                if (suplyProductQuantity == null)
                {
                    suplyProductQuantity = new SuplyProductQuantity();
                    suplyProductQuantity.CoffeeRoomId = coffeeroomno;
                    suplyProductQuantity.SuplyProductId = inv.SuplyProductId;
                    entities.SuplyProductQuantities.Add(suplyProductQuantity);
                }
                suplyProductQuantity.Quantity = inv.QuantityAfer;
            }
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleItemInventoryEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleItemInventoryEnabled([FromUri]int coffeeroomno, int suplyProductId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var suplyProduct = entities.SupliedProducts.First(s => s.Id == suplyProductId);
            suplyProduct.InventoryEnabled = !suplyProduct.InventoryEnabled;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetInventoryReports)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetInventoryReports([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var reports = entities.InventoryReports.Where(s => s.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, reports);
        }

        [Route(RoutesConstants.GetInventoryReportDetails)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetInventoryReportDetails([FromUri]int coffeeroomno, int reportId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var items = entities.InventoryReportItems.Where(s => s.InventoryReportId == reportId && s.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
        
        [Route(RoutesConstants.GetInventoryItemsForShiftToUpdate)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetInventoryItemsForShiftToUpdate([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var result = new List<InventoryItemsInfoForShiftDTO>();
            
            var entities = new CoffeeRoomEntities();
            var orders = entities.AutoOrders.Include(o => o.SuplyProductOrderItems).Include(i => i.SuplyProductOrderItems.Select(s => s.SupliedProduct)).ToList();
            foreach (var order in orders)
            {
                var updatedDate = DateTime.Now.AddDays(-1);
                if (order.IsActive && order.DayOfWeek == updatedDate.DayOfWeek)
                {
                    var suplyProducts = new List<Models.SupliedProduct>();
                    foreach (var sp in order.SuplyProductOrderItems)
                    {
                        var quantity = entities.SuplyProductQuantities.FirstOrDefault(q =>
                            q.SuplyProductId == sp.Id && q.CoffeeRoomId == coffeeroomno);
                        if (sp.ShouldUpdateQuantityBeforeOrder && quantity.LastUpdatedDate < updatedDate)
                        {
                            suplyProducts.Add(sp.SupliedProduct.ToDTO(coffeeroomno));
                        }
                    }

                    if (suplyProducts.Any())
                    {
                        result.Add(new InventoryItemsInfoForShiftDTO()
                        {
                            Items = suplyProducts,
                            AutoOrderId = order.Id
                        });
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [Route(RoutesConstants.SendInventoryItemsForShiftToUpdate)]
        [HttpPost]
        public async Task<HttpResponseMessage> SendInventoryItemsForShiftToUpdate([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Models.SupliedProduct>>(request);

            var entities = new CoffeeRoomEntities();
            foreach (var item in items)
            {
                var quantity = entities.SuplyProductQuantities
                    .First(q => q.SuplyProductId == item.Id && q.CoffeeRoomId == coffeeroomno);
                quantity.Quantity = item.Quatity.Value;
                quantity.LastUpdatedDate = DateTime.Now;
                entities.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

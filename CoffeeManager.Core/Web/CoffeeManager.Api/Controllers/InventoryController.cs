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
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        public async Task<HttpResponseMessage> Get([FromUri]int coffeeroomno, [FromUri]int productType, [FromUri]bool isPoliceSale)
        {
            var entities = new CoffeeRoomDbEntities();
            var products = entities.Products.Where(p => p.CoffeeRoomNo == coffeeroomno && p.ProductType.Value == productType);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("api/products/saleproduct")]
        [HttpPut]
        public async Task<HttpResponseMessage> SaleProduct([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sale = JsonConvert.DeserializeObject<Sale>(request);
            try
            {
                var entities = new CoffeeRoomDbEntities();
                entities.Sales.Add(sale);
                var currentShift = entities.Shifts.First(s => s.Id == sale.ShiftId.Value);
                currentShift.CurrentAmount += sale.Product1.Price;
                currentShift.TotalAmount += sale.Product1.Price;
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
            
        }

        [Route("api/products/deletesale")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteSale([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sale = JsonConvert.DeserializeObject<Sale>(request);
            var entities = new CoffeeRoomDbEntities();
            var saleDb =
                entities.Sales.LastOrDefault(
                    s => s.CoffeeRoomNo == coffeeroomno && s.Product == sale.Product && s.ShiftId == sale.ShiftId);
            if (saleDb != null)
            {
                entities.Sales.Remove(saleDb);
                await entities.SaveChangesAsync();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

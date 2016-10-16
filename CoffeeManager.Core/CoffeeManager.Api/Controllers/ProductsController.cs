using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AttributeRouting.Helpers;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        public async Task<HttpResponseMessage> Get([FromUri]int coffeeroomno, [FromUri]int productType)
        {
            var entities = new  CoffeeRoomEntities();
            var products = entities.Products.Where(p => p.CoffeeRoomNo == coffeeroomno && p.ProductType.Value == productType).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("api/products/saleproduct")]
        [HttpPut]
        public async Task<HttpResponseMessage> SaleProduct([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sale = JsonConvert.DeserializeObject<Models.Sale>(request);
            try
            {
                var entities = new  CoffeeRoomEntities();
                entities.Sales.Add(DbMapper.Map(sale));
                var currentShift = entities.Shifts.First(s => s.Id == sale.ShiftId);
                currentShift.CurrentAmount += sale.Amount;
                currentShift.TotalAmount += sale.Amount;
                var product = entities.Products.First(p => p.Id == sale.Product);
                if (product.SuplyProductId.HasValue)
                {
                    var suplyProduct = entities.SupliedProducts.First(p => p.Id == product.SuplyProductId.Value);
                    suplyProduct.Amount -= 1;
                }

                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
            }
            
        }

        [Route("api/products/deletesale")]
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteSale([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sale = JsonConvert.DeserializeObject<Sale>(request);
            var entities = new  CoffeeRoomEntities();
            var saleDb = entities.Sales.First(s => s.CoffeeRoomNo == coffeeroomno && s.Id == sale.Id);
            saleDb.IsRejected = true;
            
            var product = entities.Products.First(p => p.Id == sale.Product);
            if (product.SuplyProductId.HasValue)
            {
                var suplyProduct = entities.SupliedProducts.First(p => p.Id == product.SuplyProductId.Value);
                suplyProduct.Amount += 1;
            }

            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

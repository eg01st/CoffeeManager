using System;
using System.Collections.Generic;
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
        public async Task<HttpResponseMessage> Get([FromUri]string coffeeroomno, [FromUri]ProductType productType, [FromUri]bool isPoliceSale)
        {
            switch (productType)
            {
                    case ProductType.Coffee:

                        foreach (var product in mock)
                        {
                            product.IsPoliceSale = isPoliceSale;
                        }

                    return Request.CreateResponse(HttpStatusCode.OK, mock);
                    break;
                    case ProductType.Tea:
                    return Request.CreateResponse(HttpStatusCode.OK, teamock);
                    break;
                default:
                    return Request.CreateResponse(HttpStatusCode.OK, mock);
                    break;
            }
        }

        [Route("api/products/saleproduct")]
        [HttpPut]
        public async Task<HttpResponseMessage> SaleProduct([FromUri]string coffeeroomno, [FromUri]int shiftId, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(request);

            return Request.CreateResponse(HttpStatusCode.OK, mock);
        }

        [Route("api/products/deletesale")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteSale([FromUri]string coffeeroomno, [FromUri]int shiftId, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(request);

            return Request.CreateResponse(HttpStatusCode.OK, mock);
        }

        private Product[] teamock = new Product[]
 {
                new Product {Id = 1, Name = "tea one", Price = 3.33f},
                new Product {Id = 2, Name = "tea two", Price = 4},
                new Product {Id = 3, Name = "tea tree", Price = 65},
                new Product {Id = 4, Name = "tea four", Price = 22},
                                new Product {Id = 5, Name = "tea one", Price = 3.33f},
                new Product {Id = 6, Name = "tea two", Price = 4},
                new Product {Id = 7, Name = "tea tree", Price = 65},
                new Product {Id = 8, Name = "tea four", Price = 22},
                                new Product {Id = 9, Name = "tea one", Price = 3.33f},
                new Product {Id = 10, Name = "tea two", Price = 4},
                new Product {Id = 11, Name = "tea tree", Price = 65},
                new Product {Id = 12, Name = "tea four", Price = 22},
                 new Product {Id = 1, Name = "tea one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
                 new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
 };

        private Product[] mock = new Product[]
         {
                new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
                 new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
                 new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
         };
    }
}

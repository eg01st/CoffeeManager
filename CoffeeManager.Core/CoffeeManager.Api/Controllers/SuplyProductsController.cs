using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class SuplyProductsController : ApiController
    {
        [Route("api/suplyproducts/getproducts")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSuplyProducts([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entites = new CoffeeRoomEntities();
            var items = entites.SupliedProducts.ToList().Select(p => p.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [Route("api/suplyproducts/addproduct")]
        [HttpPut]
        public async Task<HttpResponseMessage> AddSuplyProduct([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var sProduct = JsonConvert.DeserializeObject<Models.SupliedProduct>(request);
            var prodDb = DbMapper.Map(sProduct);
            var entites = new CoffeeRoomEntities();
            entites.SupliedProducts.Add(prodDb);
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyproducts/addsuplyrequest")]
        [HttpPut]
        public async Task<HttpResponseMessage> AddSuplyRequest([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var requests = JsonConvert.DeserializeObject<Models.SupliedProduct[]>(request);
            var entites = new CoffeeRoomEntities();
            foreach (var supliedProduct in requests)
            {
                entites.SuplyRequests.Add(new SuplyRequest()
                {
                    Date = DateTime.Now,
                    ItemCount = supliedProduct.Amount,
                    SuplyProductId = supliedProduct.Id
                });
            }
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyproducts/getsuplyrequest")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSuplyRequest([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entites = new CoffeeRoomEntities();
            var items = entites.SuplyRequests.Include("SupliedProduct").Where(s => !s.IsDone).ToList();
            var response = new List<Models.SupliedProduct>();
            foreach (var suplyRequest in items)
            {
                response.Add(new Models.SupliedProduct()
                {
                    Amount = suplyRequest.ItemCount,
                    Id = suplyRequest.Id,
                    Name = suplyRequest.SupliedProduct.Name,
                    Price = suplyRequest.SupliedProduct.Price
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/suplyproducts/processsuplyrequest")]
        [HttpPost]
        public async Task<HttpResponseMessage> ProcessSuplyRequest([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var request = await message.Content.ReadAsStringAsync();
            var requests = JsonConvert.DeserializeObject<Models.SupliedProduct[]>(request);
            foreach (var req in requests)
            {
                var entites = new CoffeeRoomEntities();
                var reqDb = entites.SuplyRequests.Include("SupliedProduct").First(s => s.Id == req.Id);
                reqDb.SupliedProduct.Price = req.Price;
                if (reqDb.ItemCount == req.Amount || reqDb.ItemCount < req.Amount)
                {
                    reqDb.IsDone = true;
                }
                else
                {
                    reqDb.ItemCount -= req.Amount;
                }
                reqDb.SupliedProduct.Amount += req.Amount;
                
                await entites.SaveChangesAsync();
            }         
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/suplyproducts/deletesuplyrequest")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteSuplyRequest([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var request = entities.SuplyRequests.FirstOrDefault(r => r.Id == id);
            if (request != null)
            {
                entities.SuplyRequests.Remove(request);
                await entities.SaveChangesAsync();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

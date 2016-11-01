using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
	public class SuplyProductsController : ApiController
	{
		[Route ("api/suplyproducts/getproducts")]
		[HttpGet]
		public async Task<HttpResponseMessage> GetSuplyProducts ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entites = new CoffeeRoomEntities ();
			var items = entites.SupliedProducts.ToList ().Select (p => p.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, items);
		}

        [Route("api/suplyproducts/getproduct")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSuplyProduct([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entites = new CoffeeRoomEntities();
            var item = entites.SupliedProducts.FirstOrDefault(p => p.Id == id);
            if(item != null)
            {
                var suplyProduct = item.ToDTO();
                var product = entites.Products.FirstOrDefault(p => p.SuplyProductId.HasValue && p.SuplyProductId.Value == id);
                if(product != null)
                {
                    suplyProduct.SalePrice = product.Price;
                }
                return Request.CreateResponse(HttpStatusCode.OK, suplyProduct);

            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find suplyId {id}");
        }

        [Route("api/suplyproducts/editSuplyProduct")]
        [HttpPost]
        public async Task<HttpResponseMessage> EditSuplyProduct([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var sProduct = JsonConvert.DeserializeObject<Models.SupliedProduct>(request);

            var entites = new CoffeeRoomEntities();
            var item = entites.SupliedProducts.FirstOrDefault(p => p.Id == sProduct.Id);
            if (item != null)
            {
                item.Name = sProduct.Name;
                item.Price = sProduct.Price;
                item.Quantity = sProduct.Quatity;
                await entites.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find suplyId {sProduct.Id}");
        }

        [Route ("api/suplyproducts/bindProductWithSuply")]
		[HttpPost]
		public async Task<HttpResponseMessage> BindProductWithSuply ([FromUri] int coffeeroomno, [FromUri] int productId, [FromUri] int suplyId, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entites = new CoffeeRoomEntities ();
			var product = entites.Products.FirstOrDefault (p => p.Id == productId);
			var suply = entites.SupliedProducts.FirstOrDefault (p => p.Id == suplyId);
			if (product != null && suply != null) {
				product.SuplyProductId = suplyId;
				await entites.SaveChangesAsync ();
				return Request.CreateResponse (HttpStatusCode.OK);
			}
			return Request.CreateErrorResponse (HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find product id {productId} or suplyId {suplyId}");
		}

		[Route ("api/suplyproducts/bindExpenseWithSuply")]
		[HttpPost]
		public async Task<HttpResponseMessage> BindExpenseWithSuply ([FromUri] int coffeeroomno, [FromUri] int exprenseId, [FromUri] int suplyId, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entites = new CoffeeRoomEntities ();
			var expense = entites.ExpenseTypes.FirstOrDefault (p => p.Id == exprenseId);
			var suply = entites.SupliedProducts.FirstOrDefault (p => p.Id == suplyId);
			if (expense != null && suply != null) {
				suply.ExprenseTypeId = exprenseId;
				await entites.SaveChangesAsync ();
				return Request.CreateResponse (HttpStatusCode.OK);
			}
			return Request.CreateErrorResponse (HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find expense id {exprenseId} or suplyId {suplyId}");
		}

		[Route ("api/suplyproducts/addproduct")]
		[HttpPut]
		public async Task<HttpResponseMessage> AddSuplyProduct ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}

			var request = await message.Content.ReadAsStringAsync ();
			var sProduct = JsonConvert.DeserializeObject<Models.SupliedProduct> (request);
			var prodDb = DbMapper.Map (sProduct);
			var entites = new CoffeeRoomEntities ();
			entites.SupliedProducts.Add (prodDb);
			await entites.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}

		[Route ("api/suplyproducts/addsuplyrequest")]
		[HttpPut]
		public async Task<HttpResponseMessage> AddSuplyRequest ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}

			var request = await message.Content.ReadAsStringAsync ();
			var requests = JsonConvert.DeserializeObject<Models.SupliedProduct []> (request);
			var entites = new CoffeeRoomEntities ();
			foreach (var supliedProduct in requests) {
				entites.SuplyRequests.Add (new SuplyRequest () {
					Date = DateTime.Now,
					Quantity = supliedProduct.Quatity,
					SuplyProductId = supliedProduct.Id
				});
			}
			await entites.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}

		[Route ("api/suplyproducts/getsuplyrequest")]
		[HttpGet]
		public async Task<HttpResponseMessage> GetSuplyRequest ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entites = new CoffeeRoomEntities ();
			var items = entites.SuplyRequests.Include ("SupliedProduct").Where (s => !s.IsDone).ToList ();
			var response = new List<Models.SupliedProduct> ();
			foreach (var suplyRequest in items) {
				response.Add (new Models.SupliedProduct () {
					Quatity = suplyRequest.Quantity,
					Id = suplyRequest.Id,
					Name = suplyRequest.SupliedProduct.Name,
					Price = suplyRequest.SupliedProduct.Price,
                    
				});
			}
			return Request.CreateResponse (HttpStatusCode.OK, response);
		}

		[Route ("api/suplyproducts/processsuplyrequest")]
		[HttpPost]
		public async Task<HttpResponseMessage> ProcessSuplyRequest ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var request = await message.Content.ReadAsStringAsync ();
			var requests = JsonConvert.DeserializeObject<Models.SupliedProduct []> (request);
			foreach (var req in requests)
            {
				var entites = new CoffeeRoomEntities ();
				var reqDb = entites.SuplyRequests.Include ("SupliedProduct").First (s => s.Id == req.Id);
				reqDb.SupliedProduct.Price = req.Price;
				if (reqDb.Quantity == req.Quatity || req.Quatity > reqDb.Quantity)
                {
					reqDb.IsDone = true;
				}
                else
                {
					reqDb.Quantity -= req.Quatity;
				}
                if (reqDb.SupliedProduct.Quantity.HasValue)
                {
                    reqDb.SupliedProduct.Quantity += req.Quatity;
                }
                else
                {
                    reqDb.SupliedProduct.Quantity = req.Quatity;
                }

				await entites.SaveChangesAsync ();
			}
			return Request.CreateResponse (HttpStatusCode.OK);
		}

		[Route ("api/suplyproducts/deletesuplyrequest")]
		[HttpDelete]
		public async Task<HttpResponseMessage> DeleteSuplyRequest ([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Sessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entities = new CoffeeRoomEntities ();
			var request = entities.SuplyRequests.FirstOrDefault (r => r.Id == id);
			if (request != null) {
				entities.SuplyRequests.Remove (request);
				await entities.SaveChangesAsync ();
			}
			return Request.CreateResponse (HttpStatusCode.OK);
		}

        [Route("api/suplyproducts/deletesuplyproduct")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteSuplyProduct([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var request = entities.SupliedProducts.FirstOrDefault(r => r.Id == id);
            if (request != null)
            {
                entities.SupliedProducts.Remove(request);
                await entities.SaveChangesAsync();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route("api/suplyproducts/getproductcalculationitems")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetProductCalculationItems([FromUri] int coffeeroomno, [FromUri] int productId, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            
            var entites = new CoffeeRoomEntities();
            var product = entites.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable,
                    "Cannot find product with id " + productId);
            }

            var result = new ProductCalculationEntity();
            result.ProductId = productId;
            result.Name = product.Name;
            result.CoffeeRoomNo = product.CoffeeRoomNo.Value;
            var items = new List<CalculationItem>();
            foreach (var productCalculation in product.ProductCalculations)
            {
                var suplyProduct = entites.SupliedProducts.First(p => p.Id == productCalculation.SuplyProductId);
                items.Add(new CalculationItem()
                {
                    CoffeeRoomNo = suplyProduct.CoffeeRoomNo.Value,
                    Id = productCalculation.Id,
                    Name = suplyProduct.Name,
                    Quantity = productCalculation.Quantity,
                    SuplyProductId = suplyProduct.Id
                });
            }
            result.SuplyProductInfo = items.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/suplyproducts/deleteproductcalculationitem")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteProductCalculationItem([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var productCalculationItem = entities.ProductCalculations.FirstOrDefault(r => r.Id == id);
            if (productCalculationItem != null)
            {
                entities.ProductCalculations.Remove(productCalculationItem);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable,
                "Cannot find product item id " + id);
        }

        [Route("api/suplyproducts/addproductcalculationitem")]
        [HttpPut]
        public async Task<HttpResponseMessage> AddProductCalculationItem([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Models.ProductCalculationEntity>(request);
            var entites = new CoffeeRoomEntities();
            foreach (var info in item.SuplyProductInfo)
            {
                entites.ProductCalculations.Add(new ProductCalculation()
                {
                   CoffeeRoomNo = coffeeroomno,
                   ProductId = item.ProductId,
                   Quantity = info.Quantity,
                   SuplyProductId = info.SuplyProductId
                });
            }
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

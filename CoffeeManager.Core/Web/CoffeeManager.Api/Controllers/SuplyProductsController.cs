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
using System.Data.Entity;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class SuplyProductsController : ApiController
	{
        [Route(RoutesConstants.GetSuplyProducts)]
		[HttpGet]
		public async Task<HttpResponseMessage> GetSuplyProducts ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			//var token = message.Headers.GetValues ("token").FirstOrDefault ();
			//if (token == null || !UserSessions.Contains (token)) {
			//	return Request.CreateResponse (HttpStatusCode.Forbidden);
			//}
			var entites = new CoffeeRoomEntities ();
            var items = entites.SupliedProducts.Include(p => p.SuplyProductQuantities).Include(s => s.ExpenseType).Where(s => !s.Removed).ToList ().Select (p => p.ToDTO (coffeeroomno));
			return Request.CreateResponse (HttpStatusCode.OK, items);
		}

        [Route(RoutesConstants.GetSuplyProduct)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSuplyProduct([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var entites = new CoffeeRoomEntities();
            var item = entites.SupliedProducts.Include(p => p.SuplyProductQuantities).FirstOrDefault(p => p.Id == id);
            if(item != null)
            {
                var suplyProduct = item.ToDTO(coffeeroomno);
                var product = entites.Products.FirstOrDefault(p => p.SuplyProductId.HasValue && p.SuplyProductId.Value == id);
                if(product != null)
                {
                    suplyProduct.SalePrice = product.Price;
                }
                return Request.CreateResponse(HttpStatusCode.OK, suplyProduct);

            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find suplyId {id}");
        }

        [Route(RoutesConstants.EditSuplyProduct)]
        [HttpPost]
        public async Task<HttpResponseMessage> EditSuplyProduct([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sProduct = JsonConvert.DeserializeObject<Models.SupliedProduct>(request);

            var entites = new CoffeeRoomEntities();
            var item = entites.SupliedProducts.Include(s => s.SuplyProductQuantities).FirstOrDefault(p => p.Id == sProduct.Id);

            if (item != null)
            {
                var prodDb = DbMapper.Update(sProduct, item);
                var quantity = prodDb.SuplyProductQuantities.FirstOrDefault(q => q.CoffeeRoomId == coffeeroomno);
                if (quantity != null)
                {
                    quantity.Quantity = sProduct.Quatity.Value;
                }
                else
                {
                    var newQuantity = new SuplyProductQuantity()
                    {
                        CoffeeRoomId = coffeeroomno,
                        SuplyProductId = item.Id,
                        Quantity = sProduct.Quatity.Value
                    };
                    entites.SuplyProductQuantities.Add(newQuantity);
                }
                await entites.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, item.ToDTO(coffeeroomno));
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find suplyId {sProduct.Id}");
        }


		[Route(RoutesConstants.AddSuplyProduct)]
		[HttpPut]
		public async Task<HttpResponseMessage> AddSuplyProduct ([FromUri] int coffeeroomno, HttpRequestMessage message)
		{
			var request = await message.Content.ReadAsStringAsync ();
			var sProduct = JsonConvert.DeserializeObject<Models.SupliedProduct> (request);
			var prodDb = DbMapper.Map (sProduct);
			var entites = new CoffeeRoomEntities ();
			entites.SupliedProducts.Add (prodDb);
			await entites.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}

        [Route(RoutesConstants.DeleteSuplyProduct)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteSuplyProduct([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var suplyProduct = entities.SupliedProducts.FirstOrDefault(r => r.Id == id);
            if (suplyProduct != null)
            {
                suplyProduct.Removed = true;
                await entities.SaveChangesAsync();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.GetProductCalculationItems)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetProductCalculationItems([FromUri] int coffeeroomno, [FromUri] int productId, HttpRequestMessage message)
        {
            var entites = new CoffeeRoomEntities();
            var product = entites.Products.FirstOrDefault(p => p.Id == productId && p.CoffeeRoomNo == coffeeroomno);
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
                    Id = productCalculation.Id,
                    Name = suplyProduct.Name,
                    Quantity = productCalculation.Quantity,
                    SuplyProductId = suplyProduct.Id
                });
            }
            result.SuplyProductInfo = items.ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

       [Route(RoutesConstants.DeleteProductCalculationItem)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteProductCalculationItem([FromUri] int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var productCalculationItem = entities.ProductCalculations.FirstOrDefault(r => r.Id == id && r.CoffeeRoomNo == coffeeroomno);
            if (productCalculationItem != null)
            {
                entities.ProductCalculations.Remove(productCalculationItem);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable,
                "Cannot find product item id " + id);
        }

       [Route(RoutesConstants.AddProductCalculationItem)]
        [HttpPut]
        public async Task<HttpResponseMessage> AddProductCalculationItem([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
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

        [Route(RoutesConstants.UtilizeSuplyProduct)]
        [HttpPost]
        public async Task<HttpResponseMessage> UtilizeSuplyProduct([FromUri] int coffeeroomno, HttpRequestMessage message)
        {

            var request = await message.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Models.UtilizedSuplyProduct>(request);
            var entites = new CoffeeRoomEntities();
       
            var suplyProductQuantity = entites.SuplyProductQuantities.First(p => p.SuplyProductId == item.SuplyProductId && p.CoffeeRoomId == coffeeroomno);
            suplyProductQuantity.Quantity -= item.Quantity;

            var dbItem = DbMapper.Map(item);
            entites.UtilizedSuplyProducts.Add(dbItem);

            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetUtilizedSuplyProducts)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUtilizedSuplyProducts([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var entites = new CoffeeRoomEntities();

            var items = entites.UtilizedSuplyProducts.Include(u => u.SupliedProduct).Where(u => u.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }


	    [Route(RoutesConstants.TransferSuplyProduct)]
	    [HttpPost]
	    public async Task<HttpResponseMessage> TransferSuplyProduct([FromUri] int coffeeroomno, HttpRequestMessage message)
	    {
	        var request = await message.Content.ReadAsStringAsync();
	        var transferRequests = JsonConvert.DeserializeObject<IEnumerable<TransferSuplyProductRequest>>(request);

            var entites = new CoffeeRoomEntities();

	        foreach (var transferRequest in transferRequests)
	        {
	            var itemFrom = entites.SuplyProductQuantities.First(u =>
	                u.SuplyProductId == transferRequest.SuplyProductId &&
	                u.CoffeeRoomId == transferRequest.CoffeeRoomIdFrom);
	            if (itemFrom.Quantity < transferRequest.Quantity)
	            {
	                throw new Exception("Source quantity less than requested quantity");
	            }
	            itemFrom.Quantity -= transferRequest.Quantity;
	            var itemTo = entites.SuplyProductQuantities.FirstOrDefault(u =>
	                u.SuplyProductId == transferRequest.SuplyProductId &&
	                u.CoffeeRoomId == transferRequest.CoffeeRoomIdTo);
	            if (itemTo != null)
	            {
	                itemTo.Quantity += transferRequest.Quantity;
	            }
	            else
	            {
	                var quantity = new SuplyProductQuantity()
	                {
	                    CoffeeRoomId = transferRequest.CoffeeRoomIdTo,
	                    SuplyProductId = transferRequest.SuplyProductId,
	                    Quantity = transferRequest.Quantity
	                };
	                entites.SuplyProductQuantities.Add(quantity);
	            }
	        }
	        entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
	    }

    }
}

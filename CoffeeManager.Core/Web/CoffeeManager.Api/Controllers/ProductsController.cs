using System;
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
	public class ProductsController : ApiController
	{
		// GET: api/Products
		public async Task<HttpResponseMessage> Get ([FromUri]int coffeeroomno, [FromUri]int productType)
		{
			var entities = new CoffeeRoomEntities ();
			var products = entities.Products.Where (p => p.CoffeeRoomNo == coffeeroomno && p.ProductType.Value == productType).ToList ().Select (s => s.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, products);
		}

        [Route (RoutesConstants.GetAllProducts)]
		[HttpGet]
		public async Task<HttpResponseMessage> GetAll ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entities = new CoffeeRoomEntities ();
			var products = entities.Products.Where (p => p.CoffeeRoomNo == coffeeroomno).ToList ().Select (s => s.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, products);
		}

        [Route(RoutesConstants.AddProduct)]
		[HttpPut]
		public async Task<HttpResponseMessage> AddProduct ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var request = await message.Content.ReadAsStringAsync ();
			var product = JsonConvert.DeserializeObject<Models.Product> (request);

			var entities = new CoffeeRoomEntities ();
			entities.Products.Add (DbMapper.Map (product));
			await entities.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}

        [Route(RoutesConstants.EditProduct)]
		[HttpPost]
		public async Task<HttpResponseMessage> EditProduct ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var request = await message.Content.ReadAsStringAsync ();
			var product = JsonConvert.DeserializeObject<Models.Product> (request);

			var entities = new CoffeeRoomEntities ();

			var prodDb = entities.Products.FirstOrDefault (p => p.Id == product.Id && p.CoffeeRoomNo == coffeeroomno);
			if (prodDb != null) {
				prodDb.Name = product.Name;
				prodDb.Price = product.Price;
				prodDb.PolicePrice = product.PolicePrice;
				prodDb.CupType = product.CupType;
				prodDb.ProductType = product.ProductType;
                prodDb.IsSaleByWeight = product.IsSaleByWeight;
				if (product.SuplyId.HasValue) {
					prodDb.SuplyProductId = product.SuplyId.Value;
				}
				await entities.SaveChangesAsync ();
				return Request.CreateResponse (HttpStatusCode.OK);
			} else {
				return Request.CreateErrorResponse (HttpStatusCode.RequestedRangeNotSatisfiable, $"Product with id {product.Id} not found");
			}
		}

        [Route (RoutesConstants.DeleteProduct)]
		[HttpDelete]
		public async Task<HttpResponseMessage> DeleteProduct ([FromUri]int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Contains (token))
            {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entities = new CoffeeRoomEntities ();
            var product = entities.Products.FirstOrDefault(p => p.Id == id && p.CoffeeRoomNo == coffeeroomno);
            if(product != null)
            {
                entities.Products.Remove(product);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"No product with id  {id}");
		}

        [Route (RoutesConstants.SaleProduct)]
		[HttpPut]
		public async Task<HttpResponseMessage> SaleProduct ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var request = await message.Content.ReadAsStringAsync ();
			var sale = JsonConvert.DeserializeObject<Models.Sale> (request);
			try
            {
				var entities = new CoffeeRoomEntities ();
				entities.Sales.Add (DbMapper.Map (sale));
                var currentShift = entities.Shifts.First(s => s.Id == sale.ShiftId);
                if (sale.IsCreditCardSale)
                {
                    currentShift.CreditCardAmount += sale.Amount;
                }
                else
                {
                    currentShift.CurrentAmount += sale.Amount;
                    currentShift.TotalAmount += sale.Amount;
                }
				
                await Task.Run( async () =>  
                {
                    using (var sContext = new CoffeeRoomEntities())
                    {
                        var product = sContext.Products.First(p => p.Id == sale.ProductId);
                        foreach (var productCalculation in product.ProductCalculations)
                        {
                            var supliedProduct =
                                sContext.SupliedProducts.First(p => p.Id == productCalculation.SuplyProductId);
                            supliedProduct.Quantity -= productCalculation.Quantity;
                            await sContext.SaveChangesAsync();
                        }
                    }
                });
				await entities.SaveChangesAsync();
				return Request.CreateResponse (HttpStatusCode.OK);
			} catch (Exception ex) {
				return Request.CreateErrorResponse (HttpStatusCode.BadRequest, ex.ToString ());
			}

		}

        [Route (RoutesConstants.DeleteSale)]
		[HttpPost]
		public async Task<HttpResponseMessage> DeleteSale ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var request = await message.Content.ReadAsStringAsync ();
			var sale = JsonConvert.DeserializeObject<Sale> (request);
			var entities = new CoffeeRoomEntities ();
			var saleDb = entities.Sales.First (s => s.CoffeeRoomNo == coffeeroomno && s.Id == sale.Id);
			saleDb.IsRejected = true;

            await Task.Run(async () =>
            {
                using (var sContext = new CoffeeRoomEntities())
                {
                    var product = sContext.Products.First(p => p.Id == saleDb.Product);
                    foreach (var productCalculation in product.ProductCalculations)
                    {
                        var supliedProduct =
                            sContext.SupliedProducts.First(p => p.Id == productCalculation.SuplyProductId);
                        supliedProduct.Quantity += productCalculation.Quantity;
                        await sContext.SaveChangesAsync();
                    }
                }
            });

            var currentShift = entities.Shifts.First (s => s.Id == sale.ShiftId);
            if(saleDb.IsCreditCardSale)
            {
                currentShift.CreditCardAmount -= saleDb.Amount;
            }
            else
            {
                currentShift.CurrentAmount -= saleDb.Amount;
                currentShift.TotalAmount -= saleDb.Amount;
            }
			

			await entities.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}


        [Route(RoutesConstants.UtilizeSale)]
        [HttpPost]
        public async Task<HttpResponseMessage> UtilizeSale([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sale = JsonConvert.DeserializeObject<Sale>(request);
            var entities = new CoffeeRoomEntities();
            var saleDb = entities.Sales.First(s => s.CoffeeRoomNo == coffeeroomno && s.Id == sale.Id);
            saleDb.IsUtilized = true;

            var currentShift = entities.Shifts.First(s => s.Id == sale.ShiftId);
            if (saleDb.IsCreditCardSale)
            {
                currentShift.CreditCardAmount -= saleDb.Amount;
            }
            else
            {
                currentShift.CurrentAmount -= saleDb.Amount;
                currentShift.TotalAmount -= saleDb.Amount;
            }

            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleProductEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleProductEnabled([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var product = entities.Products.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == id);
            if (product != null)
            {
                product.IsActive = !product.IsActive;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
	public class ProductsController : ApiController
	{

        private static readonly object _sync = new object();
		// GET: api/Products
		public async Task<HttpResponseMessage> Get ([FromUri]int coffeeroomno, [FromUri]int productType)
		{
			var entities = new CoffeeRoomEntities ();
			var products = entities.Products.Where (p => p.CoffeeRoomNo == coffeeroomno && p.ProductType.Value == productType).ToList ().Select (s => s.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, products);
		}

		[Route ("api/products/getAll")]
		[HttpGet]
		public async Task<HttpResponseMessage> Get ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Contains (token)) {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entities = new CoffeeRoomEntities ();
			var products = entities.Products.Where (p => p.CoffeeRoomNo == coffeeroomno).ToList ().Select (s => s.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, products);
		}

		[Route ("api/products/addproduct")]
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

		[Route ("api/products/editproduct")]
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

			var prodDb = entities.Products.FirstOrDefault (p => p.Id == product.Id);
			if (prodDb != null) {
				prodDb.Name = product.Name;
				prodDb.Price = product.Price;
				prodDb.PolicePrice = product.PolicePrice;
				prodDb.CupType = product.CupType;
				prodDb.ProductType = product.ProductType;
				if (product.SuplyId.HasValue) {
					prodDb.SuplyProductId = product.SuplyId.Value;
				}
				await entities.SaveChangesAsync ();
				return Request.CreateResponse (HttpStatusCode.OK);
			} else {
				return Request.CreateErrorResponse (HttpStatusCode.RequestedRangeNotSatisfiable, $"Product with id {product.Id} not found");
			}
		}

		[Route ("api/products/deleteproduct")]
		[HttpDelete]
		public async Task<HttpResponseMessage> DeleteProduct ([FromUri]int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
		{
			var token = message.Headers.GetValues ("token").FirstOrDefault ();
			if (token == null || !UserSessions.Contains (token))
            {
				return Request.CreateResponse (HttpStatusCode.Forbidden);
			}
			var entities = new CoffeeRoomEntities ();
            var product = entities.Products.FirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                entities.Products.Remove(product);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"No product with id  {id}");
		}

		[Route ("api/products/saleproduct")]
		[HttpPut]
		public async Task<HttpResponseMessage> SaleProduct ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
                var request = await message.Content.ReadAsStringAsync();
                var sale = JsonConvert.DeserializeObject<Models.Sale>(request);
                var entities = new CoffeeRoomEntities();
                entities.Sales.Add(DbMapper.Map(sale));

            lock(_sync)
            {
                var ctx = new CoffeeRoomEntities();
                var currentShift = ctx.Shifts.First(s => s.Id == sale.ShiftId);
                currentShift.CurrentAmount += sale.Amount;
                currentShift.TotalAmount += sale.Amount;
                ctx.SaveChanges();
            }
  

                await Task.Run(async () =>
               {
                   using (var sContext = new CoffeeRoomEntities())
                   {
                       var product = sContext.Products.First(p => p.Id == sale.Product);
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

		}

		[Route ("api/products/deletesale")]
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

            lock (_sync)
            {
                var ctx = new CoffeeRoomEntities();
                var currentShift = ctx.Shifts.First(s => s.Id == sale.ShiftId);
                currentShift.CurrentAmount -= saleDb.Amount;
                currentShift.TotalAmount -= saleDb.Amount;
                ctx.SaveChanges();
            }

			await entities.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}


        [Route("api/products/UtilizeSale")]
        [HttpPost]
        public async Task<HttpResponseMessage> UtilizeSale([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var sale = JsonConvert.DeserializeObject<Sale>(request);
            var entities = new CoffeeRoomEntities();
            var saleDb = entities.Sales.First(s => s.CoffeeRoomNo == coffeeroomno && s.Id == sale.Id);
            saleDb.IsUtilized = true;
            lock (_sync)
            {
                var ctx = new CoffeeRoomEntities();
                var currentShift = ctx.Shifts.First(s => s.Id == sale.ShiftId);
                currentShift.CurrentAmount -= saleDb.Amount;
                currentShift.TotalAmount -= saleDb.Amount;
                ctx.SaveChanges();
            }

            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

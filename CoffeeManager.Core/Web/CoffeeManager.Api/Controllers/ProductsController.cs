using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
	{

        private static readonly object ShiftAmountLock = new object();
		// GET: api/Products
		public async Task<HttpResponseMessage> Get ([FromUri]int coffeeroomno, int productType)
		{
			var entities = new CoffeeRoomEntities ();
			var products = entities.Products.Include(i => i.Category).Where (p => p.CategoryId == productType && !p.Removed).ToList ().Select (s => s.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, products);
		}

        [Route (RoutesConstants.GetAllProducts)]
		[HttpGet]
		public async Task<HttpResponseMessage> GetAll ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var entities = new CoffeeRoomEntities ();
			var products = entities.Products.Include(i => i.Category).Where (p => !p.Removed).ToList ().Select (s => s.ToDTO ());
			return Request.CreateResponse (HttpStatusCode.OK, products);
		}

	    [Route(RoutesConstants.GetProduct)]
	    [HttpGet]
	    public async Task<HttpResponseMessage> GetProduct([FromUri]int coffeeroomno, [FromUri]int productId, HttpRequestMessage message)
	    {
	        var entities = new CoffeeRoomEntities();
	        var product = entities.Products.FirstOrDefault(p => p.Id == productId);
	        if (product == null)
	        {
	            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"No product with id  {productId}");
            }

	        var dto = product.ToDetailsDTO();

	        if (product.IsPercentPaymentEnabled)
	        {
	            var strategies = entities.ProductPaymentStrategies.Where(s => s.ProductId == productId).ToList()
	                .Select(s => s.ToDTO()).ToList();
	            dto.ProductPaymentStrategies = strategies;
	        }
	        return Request.CreateResponse(HttpStatusCode.OK, dto);
        }

        [Route(RoutesConstants.AddProduct)]
		[HttpPut]
		public async Task<HttpResponseMessage> AddProduct ([FromUri]int coffeeroomno, HttpRequestMessage message)
		{
			var request = await message.Content.ReadAsStringAsync ();
			var product = JsonConvert.DeserializeObject<ProductDetaisDTO> (request);
            product.CoffeeRoomNo = coffeeroomno;
			var entities = new CoffeeRoomEntities ();
			entities.Products.Add (product.Map());
			await entities.SaveChangesAsync ();
			return Request.CreateResponse (HttpStatusCode.OK);
		}

        [Route(RoutesConstants.EditProduct)]
		[HttpPost]
		public async Task<HttpResponseMessage> EditProduct ([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
			var request = await message.Content.ReadAsStringAsync ();
			var product = JsonConvert.DeserializeObject<ProductDetaisDTO> (request);

			var entities = new CoffeeRoomEntities ();

			var prodDb = entities.Products.FirstOrDefault (p => p.Id == product.Id);
			if (prodDb != null) {
				prodDb.Name = product.Name;
				prodDb.Price = product.Price;
				prodDb.PolicePrice = product.PolicePrice;
				prodDb.CupType = product.CupType;
                prodDb.IsSaleByWeight = product.IsSaleByWeight;
				prodDb.CategoryId = product.CategoryId;
			    prodDb.Color = product.Color;
			    prodDb.Description = product.Description;
			    prodDb.IsPercentPaymentEnabled = product.IsPercentPaymentEnabled;
				if (product.SuplyId.HasValue)
                {
					prodDb.SuplyProductId = product.SuplyId.Value;
				}
			    if (product.ProductPaymentStrategies != null)
			    {
			        foreach (var strategy in product.ProductPaymentStrategies)
			        {
			            var strDb = entities.ProductPaymentStrategies.FirstOrDefault(s => s.Id == strategy.Id);
			            if (strDb != null)
			            {
			                strDb.DayShiftPercent = strategy.DayShiftPercent;
			                strDb.NightShiftPercent = strategy.NightShiftPercent;
			            }
			            else
			            {
			                var newStrategy = strategy.Map();
			                entities.ProductPaymentStrategies.Add(newStrategy);
			            }
			        }
			    }
			    await entities.SaveChangesAsync ();
				return Request.CreateResponse (HttpStatusCode.OK);
			} else {
				return Request.CreateErrorResponse (HttpStatusCode.RequestedRangeNotSatisfiable, $"Product with id {product.Id} not found");
			}
		}

        [Route (RoutesConstants.DeleteProduct)]
		[HttpDelete]
		public async Task<HttpResponseMessage> DeleteProduct ([FromUri]int coffeeroomno,  int id, HttpRequestMessage message)
		{
			var entities = new CoffeeRoomEntities ();
            var product = entities.Products.FirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                product.Removed = true;
                var strategies = entities.ProductPaymentStrategies.Where(s => s.ProductId == id);
                entities.ProductPaymentStrategies.RemoveRange(strategies);
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
                lock (ShiftAmountLock)
                {
                    var entities = new CoffeeRoomEntities();
                    var saleDb = DbMapper.Map(sale);
                    entities.Sales.Add(saleDb);
                    entities.SaveChanges();

                    using (var sContext = new CoffeeRoomEntities())
                    {
                        var currentShift = sContext.Shifts.First(s => s.Id == sale.ShiftId);
                        if (sale.IsCreditCardSale)
                        {
                            currentShift.CreditCardAmount += sale.Amount;
                            currentShift.TotalCreditCardAmount += sale.Amount;
                        }
                        else
                        {
                            currentShift.CurrentAmount += sale.Amount;
                            currentShift.TotalAmount += sale.Amount;
                        }
                        sContext.SaveChanges();
                    }

                    using (var sContext = new CoffeeRoomEntities())
                    {
                        var product = sContext.Products.First(p => p.Id == sale.ProductId);
                        if (product.IsSaleByWeight)
                        {

                        }
                        foreach (var productCalculation in product.ProductCalculations)
                        {
                            var supliedProductQuantity =
                                sContext.SuplyProductQuantities
                                .FirstOrDefault(p => p.SuplyProductId == productCalculation.SuplyProductId 
                                && p.CoffeeRoomId == coffeeroomno);
                            if (supliedProductQuantity == null)
                            {
                                supliedProductQuantity = new SuplyProductQuantity();
                                supliedProductQuantity.CoffeeRoomId = coffeeroomno;
                                supliedProductQuantity.SuplyProductId = productCalculation.SuplyProductId;
                                supliedProductQuantity.Quantity = 0;
                                sContext.SuplyProductQuantities.Add(supliedProductQuantity);
                            }
                            supliedProductQuantity.Quantity -= productCalculation.Quantity;
                            sContext.SaveChanges();
                        }
                    }
                }

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
                            sContext.SuplyProductQuantities.First(p => p.SuplyProductId == productCalculation.SuplyProductId && p.CoffeeRoomId == coffeeroomno);
                        supliedProduct.Quantity += productCalculation.Quantity;
                        await sContext.SaveChangesAsync();
                    }
                }
            });

            var currentShift = entities.Shifts.First (s => s.Id == saleDb.ShiftId);
            if(saleDb.IsCreditCardSale)
            {
                currentShift.CreditCardAmount -= saleDb.Amount;
                currentShift.TotalCreditCardAmount -= saleDb.Amount;
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

            var currentShift = entities.Shifts.First(s => s.Id == saleDb.ShiftId);
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
        public async Task<HttpResponseMessage> ToggleProductEnabled([FromUri]int coffeeroomno, int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var product = entities.Products.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == id);
            if (product != null)
            {
                product.IsActive = !product.IsActive;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

	    [Route(RoutesConstants.GetAvaivalbeProductColors)]
	    [HttpGet]
	    public async Task<HttpResponseMessage> GetAvaivalbeProductColors([FromUri]int coffeeroomno)
	    {
	        return Request.CreateResponse(HttpStatusCode.OK, new string[] { "#00ABAB", "#007A82", "#009A44", "#86BC25", "#00A3E0" });
	    }
    }
}

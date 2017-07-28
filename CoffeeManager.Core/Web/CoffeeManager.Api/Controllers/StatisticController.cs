using CoffeeManager.Api.Mappers;
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
    public class StatisticController : ApiController
    {
        public async Task<HttpResponseMessage> GetSales([FromUri] int coffeeroomno, [FromUri] int id, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.GetSales(from, to, id);
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route("api/statistic/getAllSales")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllSales([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.GetAllSales(from, to).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, sales.ToDTO());
        }

        [Route("api/statistic/getExpenses")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenses([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var expenses = ctx.GetExpenses(from, to).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, expenses.ToDTO());
        }

        [Route("api/statistic/getCreditCardSales")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCreditCardSales([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.Sales.Where(s => s.IsCreditCardSale && !s.IsRejected && !s.IsUtilized && s.Time > from && s.Time < to).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route("api/statistic/getSalesByName")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetSalesByName([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<string>>(request);
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.Sales.Include(p => p.Product1).Where(s => items.Contains(s.Product1.Name) && !s.IsRejected && !s.IsUtilized && s.Time > from && s.Time < to).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        public async Task<HttpResponseMessage> GetMetroExpenses([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var expenses = ctx.GetMetroExpenses(from, to).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, expenses.ToDTO());
        }
    }
}

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
using CoffeeManager.Models;

namespace CoffeeManager.Api.Controllers
{
    public class StatisticController : ApiController
    {
        [Route(RoutesConstants.StatisticGetAllSales)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllSales([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.GetAllSales(from, to.AddDays(1), coffeeroomno).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, sales.ToDTO());
        }

        [Route(RoutesConstants.StatisticGetExpenses)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenses([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var expenses = ctx.GetExpenses(from, to.AddDays(1), coffeeroomno).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, expenses.ToDTO());
        }

        [Route(RoutesConstants.StatisticGetCreditCardSales)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCreditCardSales([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            to = to.AddDays(1);
            var sales = ctx.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.IsCreditCardSale && !s.IsRejected && !s.IsUtilized && s.Time > from && s.Time < to).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route(RoutesConstants.StatisticGetSalesByName)]
        [HttpPost]
        public async Task<HttpResponseMessage> GetSalesByName([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<string>>(request);
            to = to.AddDays(1);
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.Sales.Include(p => p.Product1).Where(s => s.CoffeeRoomNo == coffeeroomno && items.Contains(s.Product1.Name) && !s.IsRejected && !s.IsUtilized && s.Time > from && s.Time < to).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }
    }
}

using CoffeeManager.Api.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoffeeManager.Api.Controllers
{
    public class StatisticController : ApiController
    {
        public async Task<HttpResponseMessage> GetSales([FromUri] int coffeeroomno, [FromUri] int id, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var sales = ctx.GetSales(from, to, id);
            return null;
        }
        public async Task<HttpResponseMessage> GetAllSales([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            return null;
        }

        public async Task<HttpResponseMessage> GetExpenses([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var expenses = ctx.GetExpenses(from, to).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, expenses.ToDTO());
        }

        public async Task<HttpResponseMessage> GetMetroExpenses([FromUri] int coffeeroomno, [FromUri] DateTime from, [FromUri] DateTime to, HttpRequestMessage message)
        {
            var ctx = new CoffeeRoomEntities();
            var expenses = ctx.GetMetroExpenses(from, to).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, expenses.ToDTO());
        }
    }
}

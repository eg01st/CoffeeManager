using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CoffeeManager.Api.Controllers
{
    public class SuplyOrderController : ApiController
    {
        [Route("api/suplyorder/getorders")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrders([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var orders = entities.SuplyOrders.ToList().Select(s => s.ToDTO());
         
            return Request.CreateResponse(HttpStatusCode.OK, orders);            
        }

        [Route("api/suplyorder/getorderitems")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrderItems([FromUri]int coffeeroomno, [FromUri] int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var orders = entities.SuplyOrderItems.Where(o => o.SuplyOrderId == id).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }
    }
}

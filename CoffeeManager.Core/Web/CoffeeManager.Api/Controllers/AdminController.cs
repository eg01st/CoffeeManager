using CoffeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoffeeManager.Api.Controllers
{
    public class AdminController : ApiController
    {
        [Route(RoutesConstants.GetCoffeeRooms)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCoffeeRooms([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var items = entities.CoffeeRooms.ToList().Select(s => new Entity() { Id = s.Id, Name = s.Name });

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
    }
}

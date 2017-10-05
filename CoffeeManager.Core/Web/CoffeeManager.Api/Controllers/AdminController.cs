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
    [Authorize]
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

        [Route(RoutesConstants.AddCoffeeRoom)]
        [HttpPost]
        public async Task<HttpResponseMessage> AddCoffeeRoom([FromUri]int coffeeroomno, string name, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var coffeeRoom = new CoffeeRoom();
            coffeeRoom.Name = name;
            entities.CoffeeRooms.Add(coffeeRoom);
            entities.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

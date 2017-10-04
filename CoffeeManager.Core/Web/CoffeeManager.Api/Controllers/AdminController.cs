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

        [Route(RoutesConstants.GetCoffeePortionWeight)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCoffeePortionWeight([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var item = entities.CoffeePortions.FirstOrDefault(i => i.CoffeeRoomNo == coffeeroomno);
            if (item == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, default(decimal?));
            }
            return Request.CreateResponse(HttpStatusCode.OK, item.PortionWeight);
        }

        [Route(RoutesConstants.SetCoffeePortionWeight)]
        [HttpPost]
        public async Task<HttpResponseMessage> SetCoffeePortionWeight([FromUri]int coffeeroomno, decimal weight, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var item = entities.CoffeePortions.FirstOrDefault(i => i.CoffeeRoomNo == coffeeroomno);
            if (item == null)
            {
                item = new CoffeePortion();
                item.CoffeeRoomNo = coffeeroomno;
                item.PortionWeight = weight;
                entities.CoffeePortions.Add(item);
            }
            else
            {
                item.PortionWeight = weight;
            }
            entities.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

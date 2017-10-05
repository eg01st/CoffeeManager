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
            var items = entities.CoffeeRooms.Where(c => c.IsActive).ToList().Select(s => new Entity() { Id = s.Id, Name = s.Name });

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [Route(RoutesConstants.AddCoffeeRoom)]
        [HttpPost]
        public async Task<HttpResponseMessage> AddCoffeeRoom([FromUri]int coffeeroomno, dynamic model, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var coffeeRoom = new CoffeeRoom();
            coffeeRoom.Name = model.Name;
            entities.CoffeeRooms.Add(coffeeRoom);
            entities.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.DeleteCoffeeRoom)]
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteCoffeeRoom([FromUri]int coffeeroomno, dynamic model, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            int id = model.Id;
            var cofferrom =  entities.CoffeeRooms.FirstOrDefault(c => c.Id == id);
            if (cofferrom != null)
            {
                cofferrom.IsActive = false;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

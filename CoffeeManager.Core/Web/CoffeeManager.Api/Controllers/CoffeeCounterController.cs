using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class CoffeeCounterController : ApiController
    {
        [Route(RoutesConstants.GetCounters)]
        [HttpGet]
        public HttpResponseMessage GetCounters([FromUri]int coffeeroomno)
        {
            var counters = new CoffeeRoomEntities().CoffeeCounterForCoffeeRooms.ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<CoffeeCounterForCoffeeRoomDTO>>(counters, new JsonMediaTypeFormatter()) };
        }

        [Route(RoutesConstants.GetCounter)]
        [HttpGet]
        public HttpResponseMessage GetCounter([FromUri]int coffeeroomno, [FromUri]int counterId)
        {
            var ctx = new CoffeeRoomEntities();
            var counterDb = ctx.CoffeeCounterForCoffeeRooms.FirstOrDefault(c => c.Id == counterId);
            var counterDto = counterDb.ToDTO();
            return new HttpResponseMessage() { Content = new ObjectContent<CoffeeCounterForCoffeeRoomDTO>(counterDto, new JsonMediaTypeFormatter()) };
        }

        [Route(RoutesConstants.AddCounter)]
        [HttpPut]
        public HttpResponseMessage AddCounter([FromUri]int coffeeroomno, [FromBody]CoffeeCounterForCoffeeRoomDTO counter)
        {
            var entites = new CoffeeRoomEntities();
            var counterDb = counter.Map();
            entites.CoffeeCounterForCoffeeRooms.Add(counterDb);
            entites.SaveChanges();
            foreach (var cr in entites.CoffeeRooms)
            {
                entites.EnabledCoffeeCounters.Add(new EnabledCoffeeCounter()
                {
                    CounterId = counterDb.Id,
                    CoffeeRoomNo = cr.Id,
                    IsEnabled = true
                });
                entites.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, counterDb.Id);
        }

        [Route(RoutesConstants.UpdateCounter)]
        [HttpPost]
        public HttpResponseMessage UpdateCounter([FromUri]int coffeeroomno, [FromBody]CoffeeCounterForCoffeeRoomDTO counter)
        {
            var entites = new CoffeeRoomEntities();
            var counterDb = entites.CoffeeCounterForCoffeeRooms.First(u => u.Id == counter.Id);
            DbMapper.Update(counter, counterDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.DeleteCounter)]
        [HttpDelete]
        public HttpResponseMessage DeleteCounter([FromUri]int coffeeroomno, [FromUri]int counterId)
        {
            var entites = new CoffeeRoomEntities();
            var counterDb = entites.CoffeeCounterForCoffeeRooms.First(u => u.Id == counterId);
            entites.CoffeeCounterForCoffeeRooms.Remove(counterDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleCounterEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleCounterEnabled([FromUri]int coffeeroomno, int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var counter = entities.CoffeeCounterForCoffeeRooms.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == id);
            if (counter != null)
            {
                var isEnabledDb =
                    entities.EnabledCoffeeCounters.FirstOrDefault(c =>
                        c.CounterId == id && c.CoffeeRoomNo == coffeeroomno);
                if (isEnabledDb == null)
                {
                    isEnabledDb = new EnabledCoffeeCounter()
                    {
                        CounterId = id,
                        CoffeeRoomNo = coffeeroomno
                    };
                }
                isEnabledDb.IsEnabled = !isEnabledDb.IsEnabled;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

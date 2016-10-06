using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class CupsController : ApiController
    {
        public async Task<HttpResponseMessage> Put([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var cup = JsonConvert.DeserializeObject<Models.UtilizedCup>(request);
            var entities = new CoffeeRoomEntities();
            entities.UtilizedCups.Add(DbMapper.Map(cup));
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Get([FromUri] int coffeeroomno)
        {
            var entites = new CoffeeRoomEntities();
            var types = entites.CupTypes.Where(t => t.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, types);
        }
    }
}

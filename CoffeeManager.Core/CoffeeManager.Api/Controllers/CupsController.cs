using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Models;

namespace CoffeeManager.Api.Controllers
{
    public class CupsController : ApiController
    {
        public async Task<HttpResponseMessage> Put([FromUri] int coffeeroomno, [FromUri] int shiftId,
      [FromBody] int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Get([FromUri] int coffeeroomno)
        {
            var entites = new CoffeeRoomEntities();
            var types = entites.CupTypes.Where(t => t.CoffeeRoomNo == coffeeroomno);


            var res = new[]
            {
                new Cup() {Capacity = 110, Name = "110 ml", Id = 1},
                new Cup() {Capacity = 110, Name = "170 ml", Id = 2},
                new Cup() {Capacity = 110, Name = "250 ml", Id = 3},
                new Cup() {Capacity = 110, Name = "400 ml", Id = 4},
                new Cup() {Capacity = 110, Name = "Пластиковый", Id = 5},
            };
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}

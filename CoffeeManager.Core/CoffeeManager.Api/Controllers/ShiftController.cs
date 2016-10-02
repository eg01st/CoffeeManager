using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class ShiftController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var userId = JsonConvert.DeserializeObject<int>(request);
            var shift = new Shift
            {
                CoffeeRoomNo = coffeeroomno,
                IsFinished = false,
                UserId = userId
            };
            var entities = new CoffeeRoomDbEntities();
            entities.Shifts.Add(shift);
            await entities.SaveChangesAsync();

            return Request.CreateResponse<Shift>(HttpStatusCode.OK, shift);
        }

        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            try
            {
                var request = await message.Content.ReadAsStringAsync();
                var shiftId = JsonConvert.DeserializeObject<int>(request);
                
                var enities = new CoffeeRoomDbEntities();
                var shift = enities.Shifts.FirstOrDefault(s => s.Id == shiftId && s.CoffeeRoomNo == coffeeroomno);
                if (shift != null)
                {
                    shift.IsFinished = true;
                    await enities.SaveChangesAsync();
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
                }               
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }
        }

        [Route("api/shift/getCurrentShift")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentShift([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomDbEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno && s.IsFinished.Value);
            return Request.CreateResponse(HttpStatusCode.OK, shift);
        }

    }
}

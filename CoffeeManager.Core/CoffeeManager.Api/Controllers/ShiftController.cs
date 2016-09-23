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
        public async Task<HttpResponseMessage> Post(HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var userId = JsonConvert.DeserializeObject<Entity>(request).Id;

            int shiftNo = 123;
            return Request.CreateResponse<Entity>(HttpStatusCode.OK, new Entity() { Id = shiftNo });
        }

        public async Task<HttpResponseMessage> Put(HttpRequestMessage message)
        {
            try
            {
                var request = await message.Content.ReadAsStringAsync();
                var shiftId = JsonConvert.DeserializeObject<Entity>(request).Id;
                // finish shift
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }
        }
    }
}

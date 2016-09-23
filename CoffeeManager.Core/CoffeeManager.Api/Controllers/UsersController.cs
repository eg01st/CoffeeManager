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
    public class UsersController : ApiController
    {
        [Route("api/users")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string coffeeroomno)
        {
            return new HttpResponseMessage() { Content = new ObjectContent<User[]>(new[]
            {
                new User() {Id = 1, Name = "User1"},
                new User() {Id = 2, Name = "User 2"},
                new User() {Id = 3, Name = "User 3"},
            }, new JsonMediaTypeFormatter())};
        }

        [Route("api/users")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromUri]string coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(request);
            user.Id = 444;
            return Request.CreateResponse<User>(HttpStatusCode.OK, user);
        }


        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }
    }
}

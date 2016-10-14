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
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/users")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int coffeeroomno)
        {
            var users = new  CoffeeRoomEntities().Users.Where(u => u.CoffeeRoomNo == coffeeroomno).ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<Models.User>>(users, new JsonMediaTypeFormatter())};
        }

        [Route("api/users")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(request);
            var entites = new  CoffeeRoomEntities();
            entites.Users.Add(user);
            await entites.SaveChangesAsync();
            return Request.CreateResponse<User>(HttpStatusCode.OK, user);
        }

        [Route("api/users/login")]
        [HttpPost]
        public async Task<HttpResponseMessage> Login([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(request);
            var entites = new CoffeeRoomEntities();
            var user =
                entites.AdminUsers.FirstOrDefault(u => u.Name == userInfo.Login && u.Password == userInfo.Password);
            if (user != null)
            {
                var guid = Guid.NewGuid().ToString();
                UserSessions.Sessions.Add(guid);
                return Request.CreateResponse<string>(HttpStatusCode.OK, guid);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid user name or password");
        }


        [Route("api/users")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri]int coffeeroomno, [FromUri]int id)
        {
            var entities = new  CoffeeRoomEntities();
            var user = entities.Users.FirstOrDefault(u => u.Id == id && u.CoffeeRoomNo == coffeeroomno);
            if (user != null)
            {
                entities.Users.Remove(user);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"No user with such id '{id}'");
        }
    }
}

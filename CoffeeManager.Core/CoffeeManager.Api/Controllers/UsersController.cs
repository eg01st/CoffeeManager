using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/users")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int coffeeroomno)
        {
            var users = new  CoffeeRoomEntities().Users.Where(u => u.CoffeeRoomNo == coffeeroomno).ToArray();
            return new HttpResponseMessage() { Content = new ObjectContent<User[]>(users, new JsonMediaTypeFormatter())};
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

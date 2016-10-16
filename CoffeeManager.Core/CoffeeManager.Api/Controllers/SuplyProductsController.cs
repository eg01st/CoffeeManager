using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class SuplyProductsController : ApiController
    {
        [Route("api/suplyproducts/getproducts")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSuplyProducts([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entites = new CoffeeRoomEntities();
            var items = entites.SupliedProducts.ToList().Select(p => p.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [Route("api/suplyproducts/addproduct")]
        [HttpPut]
        public async Task<HttpResponseMessage> AddSuplyProduct([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var request = await message.Content.ReadAsStringAsync();
            var sProduct = JsonConvert.DeserializeObject<Models.SupliedProduct>(request);
            var prodDb = DbMapper.Map(sProduct);
            var entites = new CoffeeRoomEntities();
            entites.SupliedProducts.Add(prodDb);
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoffeeManager.Api.Controllers
{
    public class DeptController : ApiController
    {
        public async Task<HttpResponseMessage> Put([FromUri] int coffeeroomno, [FromUri] int shiftId,
            [FromBody] float amount)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Delete([FromUri] int coffeeroomno, [FromUri] int shiftId,
            [FromUri] float amount)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Get([FromUri] int coffeeroomno)
        {
            return Request.CreateResponse(HttpStatusCode.OK, 42);
        }
    }
}

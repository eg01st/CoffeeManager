using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Models;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class PaymentController : ApiController
    {
        [Route("api/payment/getcurrentshiftmoney")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentShiftMoney([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, 33.56f);
        }

        [Route("api/payment/getentiremoney")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetEntireMoney([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, 5555.56f);
        }

        [Route("api/payment/getexpenseitems")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenseItems([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var res = new Entity[]
            {
                new Entity { Id = 1, Name = "Кофе"},
                new Entity { Id = 1, Name = "Молоко"},
                new Entity { Id = 1, Name = "Панини"},
                new Entity { Id = 1, Name = "test"},
                new Entity { Id = 1, Name = "test"},
                new Entity { Id = 1, Name = "test"},
                new Entity { Id = 1, Name = "test"},
                new Entity { Id = 1, Name = "test"},
                new Entity { Id = 1, Name = "test"},
            };
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }


        [Route("api/payment")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, [FromUri]int shiftId, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var expense = JsonConvert.DeserializeObject<Expense>(request);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/payment/addnewexpensetype")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, [FromBody]string typeName)
        {

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}

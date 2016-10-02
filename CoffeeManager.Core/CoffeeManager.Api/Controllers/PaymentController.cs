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
            var entities = new CoffeeRoomDbEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno && !s.IsFinished.Value);
            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, shift.CurrentAmount);
            }

            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        [Route("api/payment/getentiremoney")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetEntireMoney([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomDbEntities();
            var shift = entities.Shifts.LastOrDefault(s => s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, shift.TotalAmount);
            }
            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        [Route("api/payment/getexpenseitems")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenseItems([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomDbEntities();
            var types = entities.ExpenseTypes.Where(t => t.CoffeeRoomNo == coffeeroomno);
            return Request.CreateResponse(HttpStatusCode.OK, types);
        }


        [Route("api/payment")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, [FromUri]int shiftId, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var expense = JsonConvert.DeserializeObject<Expense>(request);

            var entities = new CoffeeRoomDbEntities();
            entities.Expenses.Add(expense);
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId.Value);
            currentShift.TotalExprenses += expense.Amount;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/payment/addnewexpensetype")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, [FromBody]string typeName)
        {
            var entities = new CoffeeRoomDbEntities();
            var type = new ExpenseType() {CoffeeRoomNo = coffeeroomno, Name = typeName};
            entities.ExpenseTypes.Add(type);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
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
            var entities = new  CoffeeRoomEntities();
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
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).First();
            return Request.CreateResponse(HttpStatusCode.OK, shift.TotalAmount);
        }

        [Route("api/payment/getexpenseitems")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenseItems([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var types = entities.ExpenseTypes.Where(t => t.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, types);
        }


        [Route("api/payment")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var expense = JsonConvert.DeserializeObject<Models.Expense>(request);

            var entities = new  CoffeeRoomEntities();
            entities.Expenses.Add(DbMapper.Map(expense));
            var suplyProduct =
                entities.SupliedProducts.FirstOrDefault(
                    s => s.ExprenseTypeId.HasValue && s.ExprenseTypeId.Value == expense.ExpenseId);
            if (suplyProduct != null)
            {
                suplyProduct.Amount += expense.ItemCount;
            }
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId);
            currentShift.TotalExprenses += expense.Amount;
            currentShift.TotalAmount -= expense.Amount;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/payment/addnewexpensetype")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, [FromUri]string typeName)
        {
            var entities = new  CoffeeRoomEntities();
            var type = new ExpenseType() {CoffeeRoomNo = coffeeroomno, Name = typeName};
            entities.ExpenseTypes.Add(type);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/payment/getShiftExpenses")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftExpenses([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Sessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var expenses = entities.Expenses.Include("ExpenseType1").Where(e => e.CoffeeRoomNo == coffeeroomno && e.ShiftId.Value == id).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }

    }
}

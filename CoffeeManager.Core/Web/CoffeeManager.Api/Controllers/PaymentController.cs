﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;
using System.Data.Entity;

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

        [Route("api/payment/toggleExpenseEnabled")]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleExpenseEnabled([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var type = entities.ExpenseTypes.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == id);
            if(type != null)
            {
                type.IsActive = !type.IsActive;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
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
                if (suplyProduct.Quantity.HasValue)
                {
                    suplyProduct.Quantity += expense.ItemCount;
                }
                else
                {
                    suplyProduct.Quantity = expense.ItemCount;
                }
            }
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId);
            currentShift.TotalExprenses += expense.Amount;
            currentShift.TotalAmount -= expense.Amount;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/payment/deleteexpense")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteExpense([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            //var token = message.Headers.GetValues("token").FirstOrDefault();
            //if (token == null || !UserSessions.Sessions.Contains(token))
            //{
            //    return Request.CreateResponse(HttpStatusCode.Forbidden);
            //}
            var entities = new CoffeeRoomEntities();
            var expense = entities.Expenses.First(e => e.Id == id && e.CoffeeRoomNo.Value == coffeeroomno);
            var suplyProduct =
                entities.SupliedProducts.FirstOrDefault(
                    s => s.ExprenseTypeId.HasValue && s.ExprenseTypeId.Value == expense.ExpenseType.Value);
            if (suplyProduct != null)
            {
                suplyProduct.Quantity -= expense.Quantity;
            }
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId);
            currentShift.TotalExprenses -= expense.Amount;
            currentShift.TotalAmount += expense.Amount;

            entities.Expenses.Remove(expense);

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
            //var token = message.Headers.GetValues("token").FirstOrDefault();
            //if (token == null || !UserSessions.Sessions.Contains(token))
            //{
            //    return Request.CreateResponse(HttpStatusCode.Forbidden);
            //}
            var entities = new CoffeeRoomEntities();
            var expenses = entities.Expenses.Include("ExpenseType1").Where(e => e.CoffeeRoomNo == coffeeroomno && e.ShiftId.Value == id).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }

        [Route("api/payment/getExpenses")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenses([FromUri]int coffeeroomno, [FromUri]DateTime from, [FromUri]DateTime to, HttpRequestMessage message)
        {
            //var token = message.Headers.GetValues("token").FirstOrDefault();
            //if (token == null || !UserSessions.Sessions.Contains(token))
            //{
            //    return Request.CreateResponse(HttpStatusCode.Forbidden);
            //}
            var entities = new CoffeeRoomEntities();
            var expenses = entities.Expenses.Include(i => i.ExpenseType1).Include(i => i.Shift).Where(e => e.CoffeeRoomNo == coffeeroomno && e.Shift.Date > from && e.Shift.Date < to).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }

        [Route("api/payment/getSalesByDateDate")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSalesByDate([FromUri]int coffeeroomno, [FromUri]DateTime from, [FromUri]DateTime to, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var sales = entities.Sales.Where(s => s.Time > from && s.Time < to && s.CoffeeRoomNo == coffeeroomno).Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);           
        }

    }
}
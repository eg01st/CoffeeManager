﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class ShiftController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, [FromUri]int userId, [FromUri] int counter)
        {
            var shiftToReturn = new Models.Shift();
            var entities = new  CoffeeRoomEntities();
            var currentShift = entities.Shifts.FirstOrDefault(s => s.IsFinished.Value == false);
            if (currentShift == null)
            {
                var shift = new Shift
                {
                    CoffeeRoomNo = coffeeroomno,
                    IsFinished = false,
                    UserId = userId,
                    Date = DateTime.Now,
                    StartCounter = counter,
                    CreditCardAmount = 0
                };
                var lastShift =
                    entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).First();
                if (lastShift != null)
                {
                    shift.TotalAmount = lastShift.RealAmount;
                    shift.StartAmount = lastShift.RealAmount;
                }

                entities.Shifts.Add(shift);
                await entities.SaveChangesAsync();
                shiftToReturn.Id = shift.Id;
                shiftToReturn.UserId = shift.UserId.Value;
            }
            else
            {
                shiftToReturn.Id = currentShift.Id;
                shiftToReturn.UserId = currentShift.UserId.Value;
            }
            return Request.CreateResponse<Models.Shift>(HttpStatusCode.OK, shiftToReturn);
        }

        [Route("api/shift/endShift")]
        [HttpPost]
        public async Task<HttpResponseMessage> EndShift([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var shiftInfo = JsonConvert.DeserializeObject<EndShiftDTO>(request);
            int shiftId = shiftInfo.ShiftId;
                
            var enities = new  CoffeeRoomEntities();
            var shift = enities.Shifts.Include(s => s.User).First(s => s.Id == shiftId && s.CoffeeRoomNo == coffeeroomno);
            shift.IsFinished = true;
            shift.RealAmount = shiftInfo.RealAmount;
            shift.EndCounter = shiftInfo.Counter;

            var diff = shift.RealAmount - shift.TotalAmount;
            var realShiftAmount = shift.CurrentAmount + diff + shift.CreditCardAmount.Value;
            bool isDayShift = shift.Date.Value.TimeOfDay.Hours < 12;

            var user = shift.User;
            var userEarnedAmount = user.SimplePayment + (realShiftAmount / 100 * (isDayShift ? user.DayShiftPersent : user.NightShiftPercent));
            user.CurrentEarnedAmount += userEarnedAmount;

            await enities.SaveChangesAsync();
            return Request.CreateResponse<Models.EndShiftUserInfo>(HttpStatusCode.OK, new EndShiftUserInfo() {EarnedAmount = userEarnedAmount, RealShiftAmount = realShiftAmount, CurrentUserAmount = user.CurrentEarnedAmount });
        }

        [Route("api/shift/getCurrentShift")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentShift([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno && !s.IsFinished.Value);
            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Models.Shift() {Id = shift.Id, UserId = shift.UserId.Value});
            }
            else
            {
                return Request.CreateResponse<Models.Shift>(HttpStatusCode.OK, null);
            }
        }

        [Route("api/shift/getShifts")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShifts([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var shifts = entities.Shifts.Include(s => s.User);
            var response = new List<ShiftInfo>();
            foreach (var shift in shifts)
            {
                var res = new ShiftInfo()
                {
                    Id = shift.Id,
                    Date = shift.Date.Value,
                    RealAmount = shift.RealAmount,
                    StartMoney = shift.StartAmount,
                    UserName = shift.User.Name,
                    TotalAmount = shift.TotalAmount,
                    ExpenseAmount = shift.TotalExprenses,
                    ShiftEarnedMoney = shift.CurrentAmount,
                    CreditCardAmount = shift.CreditCardAmount
                };
                response.Add(res);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/shift/getShiftSales")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSales([FromUri]int coffeeroomno)
        {
            var currentTime = DateTime.Now.AddMinutes(-10);
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno).Id;
            var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == shift && s.Time > currentTime ).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route("api/shift/getShiftInfo")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftInfo([FromUri]int coffeeroomno, [FromUri]int id)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == id && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {

                decimal usedCoffee = 0;
                var sales = entities.Sales.Where(s => s.ShiftId == id);

                var coffeeSales = sales.Where(s => s.Product1.ProductType == 1);
                foreach (var item in coffeeSales)
                {
                    var coffeeUsage = item.Product1.ProductCalculations.FirstOrDefault(c => c.SuplyProductId == 2);
                    if(coffeeUsage != null)
                    {
                        usedCoffee += coffeeUsage.Quantity;
                    }
                }

                var usedPortions = usedCoffee / (decimal)0.0075;

                var dto = new ShiftInfo()
                {
                    Id = shift.Id,
                    Date = shift.Date.Value,
                    RealAmount = shift.RealAmount,
                    StartMoney = shift.StartAmount,
                    UserName = shift.User.Name,
                    TotalAmount = shift.TotalAmount,
                    ExpenseAmount = shift.TotalExprenses,
                    ShiftEarnedMoney = shift.CurrentAmount,
                    StartCounter = shift.StartCounter,
                    EndCounter = shift.EndCounter,
                    UsedPortions = usedPortions                
                };
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find shift with id {id}");

        }

        [Route("api/shift/getShiftSalesById")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSalesById([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == id && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == id).ToList().Select(s => s.ToDTO());
                return Request.CreateResponse(HttpStatusCode.OK, sales);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, "Shift does not exists");
        }

        [Route("api/shift/assertShiftSales")]
        [HttpPost]
        public async Task<HttpResponseMessage> AssertShiftSales([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var saleInfo = JsonConvert.DeserializeObject<SaleStorage>(request);

            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno);
            var sales = entities.Sales.Where(s => s.ShiftId == shift.Id && s.CoffeeRoomNo == coffeeroomno);

            var dismissedSalesCount = sales.Count(s => s.IsRejected);
            var utilizedSalesCount = sales.Count(s => s.IsUtilized);
            var allSales = sales.Count();
            var ms = new Message
            {
                Type = "Info",
                Message1 =
                    $"Shift id: {shift.Id}; All sales: Tablet -  {saleInfo.Sales.Count}, DB - {allSales}; Dismissed: Tablet - {saleInfo.DismissedSales.Count}, DB - {dismissedSalesCount}; Utilized: Tablet - {saleInfo.UtilizedSales.Count}, DB - {utilizedSalesCount}"
            };
            entities.Messages.Add(ms);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
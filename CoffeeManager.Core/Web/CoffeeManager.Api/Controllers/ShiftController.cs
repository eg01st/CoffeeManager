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
    [Authorize]
    public class ShiftController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, [FromUri]int userId, [FromUri] int counter)
        {
            var shiftToReturn = new Models.Shift();
            var entities = new  CoffeeRoomEntities();
            var currentShift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno && s.IsFinished.Value == false);
            if (currentShift == null)
            {
                var shift = new Shift
                {
                    CoffeeRoomNo = coffeeroomno,
                    IsFinished = false,
                    UserId = userId,
                    Date = DateTime.Now,
                    StartCounter = counter,
                    CreditCardAmount = 0,
                };
                var lastShift =
                    entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).FirstOrDefault();
                if (lastShift != null)
                {
                    shift.TotalAmount = lastShift.RealAmount;
                    shift.StartAmount = lastShift.RealAmount;
                    if (lastShift.TotalCreditCardAmount.HasValue)
                    {
                        shift.TotalCreditCardAmount = lastShift.TotalCreditCardAmount;
                    }
                    else
                    {
                        shift.TotalCreditCardAmount = 0;
                    }
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

        [Route(RoutesConstants.EndShift)]
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
            if(userEarnedAmount < user.MinimumPayment)
            {
                userEarnedAmount = user.MinimumPayment;
            }
            user.CurrentEarnedAmount += userEarnedAmount;

            var userEarneingHistory = new UserEarningsHistory();
            userEarneingHistory.Amount = userEarnedAmount;
            userEarneingHistory.Date = shift.Date.Value;
            userEarneingHistory.IsDayShift = isDayShift;
            userEarneingHistory.ShiftId = shift.Id;
            userEarneingHistory.UserId = shift.UserId.Value;
            enities.UserEarningsHistories.Add(userEarneingHistory);

            await enities.SaveChangesAsync();
            return Request.CreateResponse<Models.EndShiftUserInfo>(HttpStatusCode.OK, new EndShiftUserInfo() {EarnedAmount = userEarnedAmount, RealShiftAmount = realShiftAmount, CurrentUserAmount = user.CurrentEarnedAmount });
        }

        [Route(RoutesConstants.GetCurrentShift)]
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

        [Route(RoutesConstants.GetShifts)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShifts([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shifts = entities.Shifts.Include(s => s.User).Where(s => s.CoffeeRoomNo == coffeeroomno);
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
                    CreditCardAmount = shift.CreditCardAmount,
                    TotalCreditCardAmount = shift.TotalCreditCardAmount
                };
                response.Add(res);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route(RoutesConstants.GetShiftSales)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSales([FromUri]int coffeeroomno)
        {
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno).Id;
            var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == shift).OrderByDescending(s => s.Id).Take(10).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route(RoutesConstants.GetShiftInfo)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftInfo([FromUri]int coffeeroomno, [FromUri]int id)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == id && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                decimal usedCoffee = 0;
                var sales = entities.Sales.Where(s => s.ShiftId == id && !s.IsRejected);

                var coffeeProductTypeId = (int)Models.ProductType.Coffee;
                var coffeeSales = sales.Where(s => s.Product1.ProductType == coffeeProductTypeId);

                var coffeeSuplyProduct = entities.SupliedProducts
                    .FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno 
                    && "Кофе".Equals(s.Name, StringComparison.OrdinalIgnoreCase));
                if (coffeeSuplyProduct != null)
                {
                    foreach (var item in coffeeSales)
                    {
                        var coffeeUsage =
                            item.Product1.ProductCalculations.FirstOrDefault(c =>
                                c.SuplyProductId == coffeeSuplyProduct.Id);
                        if (coffeeUsage != null)
                        {
                            usedCoffee += coffeeUsage.Quantity;
                        }
                    }
                }

                var dto = new ShiftInfo()
                {
                    Id = shift.Id,
                    Date = shift.Date.Value,
                    RealAmount = shift.RealAmount,
                    StartMoney = shift.StartAmount,
                    UserId = shift.UserId.Value,
                    UserName = shift.User.Name,
                    TotalAmount = shift.TotalAmount,
                    ExpenseAmount = shift.TotalExprenses,
                    ShiftEarnedMoney = shift.CurrentAmount,
                    StartCounter = shift.StartCounter,
                    EndCounter = shift.EndCounter,
                    UsedPortions = usedCoffee,
                    IsFinished = shift.IsFinished.Value
                };
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find shift with id {id}");

        }

        [Route(RoutesConstants.GetShiftSalesById)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSalesById([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == id && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == id).ToList().Select(s => s.ToDTO());
                return Request.CreateResponse(HttpStatusCode.OK, sales);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, "Shift does not exists");
        }

    }
}

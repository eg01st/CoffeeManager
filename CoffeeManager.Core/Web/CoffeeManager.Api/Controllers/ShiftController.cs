using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Helper;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class ShiftController : ApiController
    {
        public static readonly object LockEndShift = new object();

        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, int userId, HttpRequestMessage message)
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

                var request = await message.Content.ReadAsStringAsync();
                var coffeeCounters = JsonConvert.DeserializeObject<List<CoffeeCounterDTO>>(request);
                if (coffeeCounters?.Count > 0)
                {
                    entities = new CoffeeRoomEntities();
                    foreach (var coffeeCounterDTO in coffeeCounters)
                    {
                        var counterInfo = new CoffeeCounter();
                        counterInfo.CoffeeRoomNo = coffeeroomno;
                        counterInfo.ShiftId = shift.Id;
                        counterInfo.SuplyProductId = coffeeCounterDTO.SuplyProductId;
                        counterInfo.StartCounter = coffeeCounterDTO.StartCounter;
                        entities.CoffeeCounters.Add(counterInfo);
                        await entities.SaveChangesAsync();
                    }
                }

                var currentMotivation = entities.Motivations.FirstOrDefault(m => !m.EndDate.HasValue);
                if (currentMotivation != null)
                {
                    var motivationItem = new ShiftMotivation();
                    motivationItem.UserId = userId;
                    motivationItem.ShiftId = shift.Id;
                    motivationItem.ShiftScore = (decimal)Constants.ShiftRate;
                    motivationItem.Date = shift.Date.Value;
                    motivationItem.MotivationId = currentMotivation.Id;
                    entities.ShiftMotivations.Add(motivationItem);
                }

                await entities.SaveChangesAsync();
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
            lock (LockEndShift)
            {
                var enities = new CoffeeRoomEntities();
                var shift = enities.Shifts.First(s => s.Id == shiftId && s.CoffeeRoomNo == coffeeroomno);
                if (shift.IsFinished == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new EndShiftUserInfo());
                }
                shift.IsFinished = true;
                shift.RealAmount = shiftInfo.RealAmount;

                foreach (var coffeeCounter in shiftInfo.CoffeeCounters)
                {
                    var counter = enities.CoffeeCounters.FirstOrDefault(c =>
                        c.CoffeeRoomNo == coffeeroomno && c.ShiftId == shiftId &&
                        c.SuplyProductId == coffeeCounter.SuplyProductId);
                    if (counter != null)
                    {
                        counter.EndCounter = coffeeCounter.EndCounter;
                        counter.UsedPortionsCount = counter.EndCounter - counter.StartCounter;
                        enities.SaveChanges();
                    }
                }


 
                decimal amountForPartialPay = 0;
                bool isDayShift = shift.Date.Value.TimeOfDay.Hours < 12;

                var diff = shift.RealAmount - shift.TotalAmount;
                decimal realShiftAmount = shift.CurrentAmount + diff + shift.CreditCardAmount.Value;
                decimal realShiftAmountForPaymentCalculation = realShiftAmount;

                var sales = enities.Sales.Where(s => s.ShiftId == shiftId && !s.IsRejected && !s.IsUtilized).GroupBy(g => g.Product).ToList();
                foreach (var saleGroup in sales)
                {
                    var product = enities.Products.First(p => p.Id == saleGroup.Key);
                    if (product.IsPercentPaymentEnabled)
                    {
                        var strategy = enities.ProductPaymentStrategies.FirstOrDefault(s =>
                            s.ProductId == saleGroup.Key && s.CoffeeRoomId == coffeeroomno);
                        if (strategy == null)
                        {
                           continue;
                        }
                        var sum = saleGroup.Sum(s => s.Amount);
                        realShiftAmountForPaymentCalculation -= sum;
                        decimal paymentPercent = isDayShift ? strategy.DayShiftPercent : strategy.NightShiftPercent;
                        amountForPartialPay += sum.GetPercentValueOf(paymentPercent);
                    }
                }

                var user = enities.Users.Include(s => s.UserPaymentStrategies).First(u => u.Id == shift.UserId);
                var userPaymentStrategy = user.UserPaymentStrategies.First(s => s.CoffeeRoomId == coffeeroomno);

                var percent = isDayShift
                    ? userPaymentStrategy.DayShiftPersent
                    : userPaymentStrategy.NightShiftPercent;
                var userEarnedAmount = userPaymentStrategy.SimplePayment
                                       + realShiftAmountForPaymentCalculation.GetPercentValueOf(percent)
                                       + amountForPartialPay;
                if (userEarnedAmount < userPaymentStrategy.MinimumPayment)
                {
                    userEarnedAmount = userPaymentStrategy.MinimumPayment;
                }
                user.CurrentEarnedAmount += userEarnedAmount;

                var userEarneingHistory = new UserEarningsHistory();
                userEarneingHistory.Amount = userEarnedAmount;
                userEarneingHistory.Date = shift.Date.Value;
                userEarneingHistory.IsDayShift = isDayShift;
                userEarneingHistory.ShiftId = shift.Id;
                userEarneingHistory.UserId = shift.UserId.Value;
                enities.UserEarningsHistories.Add(userEarneingHistory);

                decimal motivationScore = 0;
                decimal moneyMotivationScore = 0;
                
                var currentMotivation = enities.Motivations.FirstOrDefault(m => !m.EndDate.HasValue);
                var isValidShift = (DateTime.Now - shift.Date.Value).Hours > 5;
                if (Math.Abs(diff) < Constants.MaxShiftAmountOversight && currentMotivation != null && isValidShift)
                {
                    var sevenDaysAgo = DateTime.Now.AddDays(-7);
                    var weekShifts = enities.Shifts.Where(s => s.Id != shiftId && s.CoffeeRoomNo == coffeeroomno
                                                               && s.IsFinished.Value
                                                               && s.Date > sevenDaysAgo).ToList();
                    if (isDayShift)
                    {
                        weekShifts = weekShifts.Where(w => w.Date.Value.TimeOfDay.Hours < 12).ToList();
                    }
                    else
                    {
                        weekShifts = weekShifts.Where(w => w.Date.Value.TimeOfDay.Hours > 12).ToList();
                    }

                    var maxAmount = weekShifts.Max(w =>
                    {
                        var dif = w.RealAmount - w.TotalAmount;
                        if (Math.Abs(dif) > Constants.MaxShiftAmountOversight)
                        {
                            return 0;
                        }
                        return w.CurrentAmount + dif + w.CreditCardAmount.Value;
                    });

                    var onePercentOfMaxAmount = maxAmount / 100;
                    var percentOfCurrentShift = realShiftAmount / onePercentOfMaxAmount;
                    moneyMotivationScore = percentOfCurrentShift / 100;

                    var motivationItem = enities.ShiftMotivations.FirstOrDefault(f => f.ShiftId == shiftId);
                    if (motivationItem != null)
                    {
                        motivationItem.Moneycore = moneyMotivationScore;
                        motivationScore += motivationItem.ShiftScore + moneyMotivationScore;
                    }
                }

                enities.SaveChanges();

                if (!isValidShift)
                {
                    var motivationItem = enities.ShiftMotivations.FirstOrDefault(f => f.ShiftId == shiftId);
                    if (motivationItem != null)
                    {
                        enities.ShiftMotivations.Remove(motivationItem);
                        enities.SaveChanges();
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK,
                    new EndShiftUserInfo()
                    {
                        EarnedAmount = userEarnedAmount,
                        RealShiftAmount = realShiftAmount,
                        CurrentUserAmount = user.CurrentEarnedAmount,
                        EarnedMotivationScore = motivationScore
                    });
            }
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

        [Route(RoutesConstants.GetCurrentShiftForCoffeeRoom)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentShiftForCoffeeRoom([FromUri]int coffeeroomno, int forCoffeeRoom, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == forCoffeeRoom && !s.IsFinished.Value);
            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new Models.Shift() { Id = shift.Id, UserId = shift.UserId.Value });
            }
            else
            {
                return Request.CreateResponse<Models.Shift>(HttpStatusCode.OK, null);
            }
        }

        [Route(RoutesConstants.GetShifts)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShifts([FromUri]int coffeeroomno, int skip, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shifts = entities.Shifts.OrderByDescending(s => s.Id).Include(s => s.User).Where(s => s.CoffeeRoomNo == coffeeroomno).Skip(skip).Take(50);
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
                    TotalCreditCardAmount = shift.TotalCreditCardAmount,
                    IsFinished = shift.IsFinished.Value
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
            var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == shift).OrderByDescending(s => s.Id).Take(100).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route(RoutesConstants.GetShiftInfo)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftInfo([FromUri]int coffeeroomno, int id)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == id && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                decimal usedCoffee = 0;
                var sales = entities.Sales.Where(s => s.ShiftId == id && !s.IsRejected);

                var coffeeSuplyProductsIds = entities.CoffeeCounterForCoffeeRooms.Include(c => c.EnabledCoffeeCounters).ToList()
                    .Where(s => s.EnabledCoffeeCounters.FirstOrDefault(c => c.CoffeeRoomNo == coffeeroomno)?.IsEnabled ?? false).Select(s => s.SuplyProductId).ToList();
                if (coffeeSuplyProductsIds.Any())
                {
                    foreach (var item in sales)
                    {
                        var coffeeUsage =
                            item.Product1.ProductCalculations.FirstOrDefault(c =>
                                coffeeSuplyProductsIds.Contains(c.SuplyProductId));
                        if (coffeeUsage != null)
                        {
                            usedCoffee += coffeeUsage.Quantity;
                        }
                    }
                }
                int startCounter = 0;
                int? endCounter = 0;
                if (shift.IsFinished ?? false)
                {
                    var сounters = entities.CoffeeCounters.Where(c => c.ShiftId == id).ToList();
                    startCounter = сounters.Sum(s => s.StartCounter);
                    endCounter = сounters.Sum(s => s.EndCounter);
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
                    StartCounter = startCounter,
                    EndCounter = endCounter,
                    UsedPortions = usedCoffee,
                    IsFinished = shift.IsFinished.Value
                };
                return Request.CreateResponse(HttpStatusCode.OK, dto);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, $"Cannot find shift with id {id}");

        }

        [Route(RoutesConstants.GetShiftSalesById)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSalesById([FromUri]int coffeeroomno, int id, HttpRequestMessage message)
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

        [Route(RoutesConstants.DiscardShift)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DiscardShift([FromUri]int coffeeroomno, [FromUri]int shiftId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == shiftId && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                var salesCount = entities.Sales.Count(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == shiftId && !s.IsRejected);
                if (salesCount > 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, "Sales exist");
                }

                var expenseCount = entities.Expenses.Count(e => e.ShiftId == shiftId);
                if (expenseCount > 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, "Expenses exist");
                }
                entities.Sales.RemoveRange(shift.Sales);
                entities.Shifts.Remove(shift);
                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, "Shift does not exists");
        }
    }
}

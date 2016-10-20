using System;
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
        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, [FromUri]int userId)
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
                    Date = DateTime.Now
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

        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            try
            {
                var request = await message.Content.ReadAsStringAsync();
                var shiftInfo = JsonConvert.DeserializeObject<EndShiftDTO>(request);
                int shiftId = shiftInfo.ShiftId;
                
                var enities = new  CoffeeRoomEntities();
                var shift = enities.Shifts.First(s => s.Id == shiftId && s.CoffeeRoomNo == coffeeroomno);
                shift.IsFinished = true;
                shift.RealAmount = shiftInfo.RealAmount;
                var sales = enities.Sales.Where(s => s.ShiftId == shiftId && !s.IsRejected && s.Product1.CupType.HasValue);
                var cup110 = sales.Count(s => s.Product1.CupType.Value == (int)CupTypeEnum.c110);
                var cup170 = sales.Count(s => s.Product1.CupType.Value == (int)CupTypeEnum.c170);
                var cup250 = sales.Count(s => s.Product1.CupType.Value == (int)CupTypeEnum.c250);
                var cup400 = sales.Count(s => s.Product1.CupType.Value == (int)CupTypeEnum.c400);
                var plastic = sales.Count(s => s.Product1.CupType.Value == (int)CupTypeEnum.Plastic);

                var utilizedCups = enities.UtilizedCups.Where(s => s.ShiftId == shiftId);
                var c110 = utilizedCups.Count(s => s.CupTypeId == (int)CupTypeEnum.c110);
                var c170 = utilizedCups.Count(s => s.CupTypeId == (int)CupTypeEnum.c170);
                var c250 = utilizedCups.Count(s => s.CupTypeId == (int)CupTypeEnum.c250);
                var c400 = utilizedCups.Count(s => s.CupTypeId == (int)CupTypeEnum.c400);
                var plast = utilizedCups.Count(s => s.CupTypeId == (int)CupTypeEnum.Plastic);


                var usedCups = new UsedCupsPerShift
                {
                    ShiftId = shiftId,
                    C110 = cup110 + c110,
                    C170 = cup170 + c170,
                    C250 = cup250 + c250,
                    C400 = cup400 + c400,
                    Plastic = plastic + plast,
                    CoffeeRoomNo = coffeeroomno
                };
                enities.UsedCupsPerShifts.Add(usedCups);

                var usedProducts = new UsedProductsPerShift()
                {
                    CoffeePacks = shiftInfo.CoffeePacks,
                    MilkPacks = shiftInfo.MilkPacks,
                    ShiftId = shiftId,
                    CoffeeRoomNo = shiftInfo.CoffeeRoomNo
                };
                int coffeeId = (int) Models.ExpenseTypeEnum.Coffee;
                var coffee = enities.SupliedProducts.First(e => e.ExprenseTypeId.HasValue && e.ExprenseTypeId.Value == coffeeId);
                coffee.Amount -= shiftInfo.CoffeePacks;

                int milkId = (int)Models.ExpenseTypeEnum.Milk;
                var milk = enities.SupliedProducts.First(e => e.ExprenseTypeId.HasValue && e.ExprenseTypeId.Value == milkId);
                milk.Amount -= shiftInfo.MilkPacks;

                enities.UsedProductsPerShifts.Add(usedProducts);

                await enities.SaveChangesAsync();
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest };
            }
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
            if (token == null || !UserSessions.Sessions.Contains(token))
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
                    ShiftEarnedMoney = shift.CurrentAmount
                };
                response.Add(res);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("api/shift/getShiftSales")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSales([FromUri]int coffeeroomno)
        {
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno).Id;
            var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == shift && !s.IsRejected).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }

        [Route("api/shift/getShiftSalesById")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSalesById([FromUri]int coffeeroomno, [FromUri]int id)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.Id == id && s.CoffeeRoomNo == coffeeroomno);
            if (shift != null)
            {
                var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == id && !s.IsRejected).ToList().Select(s => s.ToDTO());
                return Request.CreateResponse(HttpStatusCode.OK, sales);
            }
            return Request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, "Shift does not exists");
        }
    }
}

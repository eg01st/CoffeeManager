﻿using System;
using System.Collections.Generic;
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
            var shift = new Shift
            {
                CoffeeRoomNo = coffeeroomno,
                IsFinished = false,
                UserId = userId,
                Date = DateTime.Now
            };
            var entities = new  CoffeeRoomEntities();
            var lastShift =
                entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).First();
            if (lastShift != null)
            {
                shift.TotalAmount = lastShift.TotalAmount;
                shift.StartAmount = lastShift.TotalAmount;
            }

            entities.Shifts.Add(shift);
            await entities.SaveChangesAsync();

            return Request.CreateResponse<Models.Shift>(HttpStatusCode.OK, new Models.Shift() { Id = shift.Id, UserId = userId});
        }

        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            try
            {
                var request = await message.Content.ReadAsStringAsync();
                var shiftId = JsonConvert.DeserializeObject<int>(request);
                
                var enities = new  CoffeeRoomEntities();
                var shift = enities.Shifts.First(s => s.Id == shiftId && s.CoffeeRoomNo == coffeeroomno);
                shift.IsFinished = true;
                var sales = enities.Sales.Where(s => s.ShiftId == shiftId && s.Product1.CupType.HasValue);
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

        [Route("api/shift/getShiftSales")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftSales([FromUri]int coffeeroomno)
        {
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.First(s => !s.IsFinished.Value && s.CoffeeRoomNo == coffeeroomno).Id;
            var sales = entities.Sales.Where(s => s.CoffeeRoomNo == coffeeroomno && s.ShiftId == shift).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);
        }
    }
}

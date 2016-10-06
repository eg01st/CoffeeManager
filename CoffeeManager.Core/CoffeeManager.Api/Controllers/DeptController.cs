using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using Newtonsoft.Json;

namespace CoffeeManager.Api.Controllers
{
    public class DeptController : ApiController
    {
        public async Task<HttpResponseMessage> Put([FromUri] int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var dept = JsonConvert.DeserializeObject<Models.Dept>(request);
            var entities = new CoffeeRoomEntities();
            entities.Depts.Add(DbMapper.Map(dept));
            var shift = entities.Shifts.First(s => s.Id == dept.ShiftId && s.CoffeeRoomNo == coffeeroomno);
            if (dept.IsPaid)
            {
                shift.TotalAmount += dept.Amount;
            }
            else
            {
                shift.TotalAmount -= dept.Amount;
            }
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        public async Task<HttpResponseMessage> Get([FromUri] int coffeeroomno)
        {
            var entities = new CoffeeRoomEntities();
            var unpaidDepts = entities.Depts.Where(d => !d.IsPaid).Sum(dept => dept.Amount);
            var paidDepts = entities.Depts.Where(d => d.IsPaid).Sum(dept => dept.Amount);
            var diff = paidDepts - unpaidDepts;
            return Request.CreateResponse(HttpStatusCode.OK, diff);
        }
    }
}

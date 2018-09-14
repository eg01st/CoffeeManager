using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.StaffMotivation;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class MotivationController : ApiController
    {
        [Route(RoutesConstants.GetAllMotivationItems)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllMotivationItems([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var items = entities.ShiftMotivations.ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
        
        [Route(RoutesConstants.GetUsersMotivation)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUsersMotivation([FromUri]int coffeeroomno, [FromUri]int motivationId, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();

            var items = entities.ShiftMotivations.Include(m => m.User).Where(i => i.MotivationId == motivationId).ToList();
            var groupedByUser = items.GroupBy(g => g.User);
            var result = new List<UserMotivationDTO>();
            foreach (var userInfo in groupedByUser)
            {
                var info = new UserMotivationDTO();
                info.UserName = userInfo.Key.Name;
                info.UserId = userInfo.Key.Id;
                info.MoneyScore = userInfo.Sum(s => s.Moneycore);
                info.ShiftScore = userInfo.Sum(s => s.ShiftScore);
                info.OtherScore = userInfo.Sum(s => s.OtherScore);
                result.Add(info);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result.OrderByDescending(r => r.EntireScore));
        }
        
        [Route(RoutesConstants.StartNewMotivation)]
        [HttpPost]
        public async Task<HttpResponseMessage> StartNewMotivation([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();

            var currentMotivation = entities.Motivations.FirstOrDefault(m => !m.EndDate.HasValue);
            if (currentMotivation != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not finished motivation exists");
            }
            
            var motivation = new Motivation();
            motivation.StartDate = DateTime.Now;

            entities.Motivations.Add(motivation);
            entities.SaveChanges();
            
            return Request.CreateResponse(HttpStatusCode.OK, motivation.ToDTO());
        }

        [Route(RoutesConstants.FinishMotivation)]
        [HttpPost]
        public async Task<HttpResponseMessage> FinishMotivation([FromUri]int coffeeroomno, [FromUri]int motivationId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();

            var currentMotivation = entities.Motivations.FirstOrDefault(m => m.Id == motivationId);
            if (currentMotivation == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Motivation not exists");
            }

            currentMotivation.EndDate = DateTime.Now;
            
            entities.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetCurrentMotivation)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentMotivation([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var currentMotivation = entities.Motivations.FirstOrDefault(m => !m.EndDate.HasValue);
            if (currentMotivation != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, currentMotivation.ToDTO());
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
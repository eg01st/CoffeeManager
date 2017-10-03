using System;
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
using System.Data.Entity;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [Route(RoutesConstants.GetUsers)]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int coffeeroomno)
        {
            var users = new  CoffeeRoomEntities().Users.Where(u => u.CoffeeRoomNo == coffeeroomno).ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<Models.User>>(users, new JsonMediaTypeFormatter())};
        }

        [Route(RoutesConstants.GetUser)]
        [HttpGet]
        public HttpResponseMessage GetUser([FromUri]int coffeeroomno, [FromUri]int userId)
        {
            var user = new CoffeeRoomEntities().Users.FirstOrDefault(u => u.CoffeeRoomNo == coffeeroomno && u.Id == userId)?.ToDTO();
            return new HttpResponseMessage() { Content = new ObjectContent<Models.User>(user, new JsonMediaTypeFormatter()) };
        }

        [Route(RoutesConstants.AddUser)]
        [HttpPut]
        public async Task<HttpResponseMessage> AddUser([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Models.User>(request);
            var entites = new  CoffeeRoomEntities();
            var userDb = DbMapper.Map(user);
            entites.Users.Add(userDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, userDb.Id);
        }

        [Route(RoutesConstants.UpdateUser)]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUser([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Models.User>(request);
            var entites = new CoffeeRoomEntities();
            var userDb = entites.Users.FirstOrDefault(u => u.CoffeeRoomNo == coffeeroomno && u.Id == user.Id);
            userDb =  DbMapper.Update(user, userDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.PenaltyUser)]
        [HttpPost]
        public async Task<HttpResponseMessage> PenaltyUser([FromUri]int coffeeroomno, [FromUri]int userId, [FromUri]decimal amount, [FromUri]string reason, HttpRequestMessage message)
        {
            var entites = new CoffeeRoomEntities();
            var userDb = entites.Users.First(u => u.CoffeeRoomNo == coffeeroomno && u.Id == userId);
            userDb.CurrentEarnedAmount -= amount;
            var penalty = new UserPenalty();
            penalty.Date = DateTime.Now;
            penalty.Amount = amount;
            penalty.Reason = reason;
            userDb.UserPenalties.Add(penalty);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.DismisPenalty)]
        [HttpPost]
        public async Task<HttpResponseMessage> DismisPenalty([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var entites = new CoffeeRoomEntities();
            var penalty = entites.UserPenalties.Include(u => u.User).First(u => u.Id == id);
            penalty.User.CurrentEarnedAmount += penalty.Amount;
            await entites.SaveChangesAsync();

            entites = new CoffeeRoomEntities();
            penalty = entites.UserPenalties.Include(u => u.User).First(u => u.Id == id);
            entites.UserPenalties.Remove(penalty);
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.PaySalary)]
        [HttpPost]
        public async Task<HttpResponseMessage> PaySalary([FromUri]int coffeeroomno, [FromUri]int userId, [FromUri]int currentShifId, HttpRequestMessage message)
        {
            var entites = new CoffeeRoomEntities();
            var user = entites.Users.FirstOrDefault(u => u.Id == userId && u.CoffeeRoomNo == coffeeroomno);
            if(user != null)
            {
                var expense = new Expense();
                expense.ExpenseType = user.ExpenceId;
                expense.Amount = user.CurrentEarnedAmount;
                expense.CoffeeRoomNo = coffeeroomno;
                expense.Quantity = 1;
                expense.ShiftId = currentShifId;
                entites.Expenses.Add(expense);

                user.EntireEarnedAmount += user.CurrentEarnedAmount;
                user.CurrentEarnedAmount = 0;

                var currentShift = entites.Shifts.First(s => s.Id == currentShifId);
                currentShift.TotalExprenses += expense.Amount;
                currentShift.TotalAmount -= expense.Amount;

                await entites.SaveChangesAsync();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleUserEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleUserEnabled([FromUri]int coffeeroomno, [FromUri]int userId, HttpRequestMessage message)
        {         
            var entites = new CoffeeRoomEntities();
            var user = entites.Users.FirstOrDefault(u => u.CoffeeRoomNo == coffeeroomno && u.Id == userId);
            if(user != null)
            {
                user.IsActive = !user.IsActive;
                await entites.SaveChangesAsync();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.Login)]
        [HttpPost]
        public async Task<HttpResponseMessage> Login([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(request);
            var entites = new CoffeeRoomEntities();
            var user =
                entites.AdminUsers.FirstOrDefault(u => u.Name == userInfo.Login && u.Password == userInfo.Password);
            if (user != null)
            {
                var guid = Guid.NewGuid().ToString();
                return Request.CreateResponse<string>(HttpStatusCode.OK, guid);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid user name or password");
        }


        [Route(RoutesConstants.DeleteUser)]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri]int coffeeroomno, [FromUri]int userId)
        {
            var entities = new  CoffeeRoomEntities();
            var user = entities.Users.FirstOrDefault(u => u.Id == userId && u.CoffeeRoomNo == coffeeroomno);
            if (user != null)
            {
                entities.Users.Remove(user);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"No user with such id '{userId}'");
        }
    }
}

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

namespace CoffeeManager.Api.Controllers
{
    public class UsersController : ApiController
    {
        [Route("api/users")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int coffeeroomno)
        {
            var users = new  CoffeeRoomEntities().Users.Where(u => u.CoffeeRoomNo == coffeeroomno).ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<Models.User>>(users, new JsonMediaTypeFormatter())};
        }

        [Route("api/users")]
        [HttpPut]
        public async Task<HttpResponseMessage> Post([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var request = await message.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Models.User>(request);
            var entites = new  CoffeeRoomEntities();
            entites.Users.Add(DbMapper.Map(user));
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/users/paySalary")]
        [HttpPut]
        public async Task<HttpResponseMessage> PaySalary([FromUri]int coffeeroomno, [FromUri]int userId, [FromUri]int currentShifId, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
           
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

                await entites.SaveChangesAsync();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/users/disable")]
        [HttpPost]
        public async Task<HttpResponseMessage> DisableUser([FromUri]int coffeeroomno, [FromUri]int userId, HttpRequestMessage message)
        {
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
           
            var entites = new CoffeeRoomEntities();
            var user = entites.Users.FirstOrDefault(u => u.CoffeeRoomNo == coffeeroomno && u.Id == userId);
            if(user != null)
            {
                entites.Users.Remove(user);
                await entites.SaveChangesAsync();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/users/login")]
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
                UserSessions.AddSession(user.Id, guid);
                return Request.CreateResponse<string>(HttpStatusCode.OK, guid);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid user name or password");
        }


        [Route("api/users")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri]int coffeeroomno, [FromUri]int id)
        {
            var entities = new  CoffeeRoomEntities();
            var user = entities.Users.FirstOrDefault(u => u.Id == id && u.CoffeeRoomNo == coffeeroomno);
            if (user != null)
            {
                entities.Users.Remove(user);
                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"No user with such id '{id}'");
        }
    }
}

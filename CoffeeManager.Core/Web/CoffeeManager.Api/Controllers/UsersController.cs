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
using CoffeeManager.Models.Data.DTO.User;
using CoffeeManager.Models.User;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [Route(RoutesConstants.GetUsers)]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int coffeeroomno)
        {
            var users = new  CoffeeRoomEntities().Users.ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<UserDTO>>(users, new JsonMediaTypeFormatter())};
        }

        [Route(RoutesConstants.GetUser)]
        [HttpGet]
        public HttpResponseMessage GetUser([FromUri]int coffeeroomno, int userId)
        {
            var user = new CoffeeRoomEntities().Users.Include(u => u.UserPaymentStrategies).FirstOrDefault(u => u.Id == userId)?.ToDTO();
            return new HttpResponseMessage() { Content = new ObjectContent<UserDTO>(user, new JsonMediaTypeFormatter()) };
        }

        [Route(RoutesConstants.AddUser)]
        [HttpPut]
        public async Task<HttpResponseMessage> AddUser([FromUri]int coffeeroomno, [FromBody]UserDTO user)
        {
            var entites = new  CoffeeRoomEntities();
            var userDb = DbMapper.Map(user);
            entites.Users.Add(userDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, userDb.Id);
        }

        [Route(RoutesConstants.UpdateUser)]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUser([FromUri]int coffeeroomno, [FromBody]UserDTO user)
        {
            var entites = new CoffeeRoomEntities();
            var userDb = entites.Users.First(u => u.Id == user.Id);
            userDb =  DbMapper.Update(user, userDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.PenaltyUser)]
        [HttpPost]
        public async Task<HttpResponseMessage> PenaltyUser([FromUri]int coffeeroomno, [FromBody] PenaltyUserDTO dto)
        {
            var entites = new CoffeeRoomEntities();
            var userDb = entites.Users.First(u => u.Id == dto.UserId);
            userDb.CurrentEarnedAmount -= dto.Amount;
            var penalty = new UserPenalty();
            penalty.Date = DateTime.Now;
            penalty.Amount = dto.Amount;
            penalty.Reason = dto.Reason;
            userDb.UserPenalties.Add(penalty);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.DismisPenalty)]
        [HttpPost]
        public async Task<HttpResponseMessage> DismisPenalty([FromUri]int coffeeroomno, [FromBody] DismissPenaltyDTO dto)
        {
            var entites = new CoffeeRoomEntities();
            var penalty = entites.UserPenalties.Include(u => u.User).First(u => u.Id == dto.PenaltyId);
            penalty.User.CurrentEarnedAmount += penalty.Amount;
            await entites.SaveChangesAsync();

            entites = new CoffeeRoomEntities();
            penalty = entites.UserPenalties.Include(u => u.User).First(u => u.Id == dto.PenaltyId);
            entites.UserPenalties.Remove(penalty);
            await entites.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.PaySalary)]
        [HttpPost]
        public async Task<HttpResponseMessage> PaySalary([FromUri]int coffeeroomno, PaySalaryDTO dto)
        {
            var entites = new CoffeeRoomEntities();
            var user = entites.Users.FirstOrDefault(u => u.Id == dto.UserId);
            if(user != null)
            {

                var currentShift = entites.Shifts.First(s => s.CoffeeRoomNo == dto.CoffeeRoomIdToPay && !s.IsFinished.Value);
                currentShift.TotalExprenses += user.CurrentEarnedAmount;
                currentShift.TotalAmount -= user.CurrentEarnedAmount;

                var expense = new Expense();
                expense.ExpenseType = user.ExpenceId;
                expense.Amount = user.CurrentEarnedAmount;
                expense.CoffeeRoomNo = dto.CoffeeRoomIdToPay;
                expense.Quantity = 1;
                expense.ShiftId = currentShift.Id;
                expense.IsUserSalaryPayment = true;
                expense.UserId = dto.UserId;
                entites.Expenses.Add(expense);

                user.EntireEarnedAmount += user.CurrentEarnedAmount;
                user.CurrentEarnedAmount = 0;

                await entites.SaveChangesAsync();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleUserEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleUserEnabled([FromUri]int coffeeroomno,ToggleUserEnabledDTO dto)
        {         
            var entites = new CoffeeRoomEntities();
            var user = entites.Users.FirstOrDefault(u => u.Id == dto.UserId);
            if(user != null)
            {
                user.IsActive = !user.IsActive;
                await entites.SaveChangesAsync();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetSalaryAmountToPay)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSalaryAmountToPay([FromUri]int coffeeroomno)
        {
            var entites = new CoffeeRoomEntities();
            var amount = entites.Users.Where(u => u.IsActive).Sum(s => s.CurrentEarnedAmount);
           
            return Request.CreateResponse(HttpStatusCode.OK, amount);
        }
    }
}

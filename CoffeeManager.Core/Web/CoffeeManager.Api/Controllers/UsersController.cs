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
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
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
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            var request = await message.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Models.User>(request);
            var entites = new CoffeeRoomEntities();
            var userDb = entites.Users.FirstOrDefault(u => u.CoffeeRoomNo == coffeeroomno && u.Id == user.Id);
            userDb =  DbMapper.Update(user, userDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }



        [Route(RoutesConstants.PaySalary)]
        [HttpPost]
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
            var token = message.Headers.GetValues("token").FirstOrDefault();
            if (token == null || !UserSessions.Contains(token))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
           
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
                UserSessions.AddSession(user.Id, guid);
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

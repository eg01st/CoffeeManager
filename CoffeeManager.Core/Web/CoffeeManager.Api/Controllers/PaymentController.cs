using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using Newtonsoft.Json;
using System.Data.Entity;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class PaymentController : ApiController
    {
        [Route(RoutesConstants.GetCurrentShiftMoney)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentShiftMoney([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno && !s.IsFinished.Value);
            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, shift.CurrentAmount);
            }

            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        [Route(RoutesConstants.GetEntireMoney)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetEntireMoney([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var shift = entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).FirstOrDefault();

            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, shift.TotalAmount);
            }

            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        [Route(RoutesConstants.GetCreditCardEntireMoney)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCreditCardEntireMoney([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).FirstOrDefault();

            if (shift != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, shift.TotalCreditCardAmount);
            }

            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        [Route(RoutesConstants.GetExpenseItems)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenseItems([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new  CoffeeRoomEntities();
            var types = entities.ExpenseTypes.Where(t => !t.IsRemoved).Include(i => i.SupliedProducts).Include(i => i.SupliedProducts.Select(s => s.SuplyProductQuantities))
                .ToList().Select(s => s.ToDTO(coffeeroomno));
            return Request.CreateResponse(HttpStatusCode.OK, types);
        }

        [Route(RoutesConstants.RemoveExpenseType)]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveExpenseType([FromUri]int coffeeroomno, [FromUri] int expenseTypeId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var type = entities.ExpenseTypes.First(t => t.Id == expenseTypeId);
            type.IsRemoved = true;
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleExpenseEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleExpenseEnabled([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var type = entities.ExpenseTypes.FirstOrDefault(t => t.Id == id);
            if(type != null)
            {
                type.IsActive = !type.IsActive;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.MapExpenseToSuplyProduct)]
        [HttpPost]
        public async Task<HttpResponseMessage> MapExpenseToSuplyProduct([FromUri]int coffeeroomno, [FromUri]int expenseTypeId, [FromUri]int suplyProductId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var sp = entities.SupliedProducts.FirstOrDefault(t => t.Id == suplyProductId);
            if (sp != null)
            {
                sp.ExprenseTypeId = expenseTypeId;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetMappedSuplyProductsToExpense)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetMappedSuplyProductsToExpense([FromUri]int coffeeroomno, [FromUri]int expenseTypeId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var sp = entities.SupliedProducts.Include(p => p.SuplyProductQuantities).Where(t => t.ExprenseTypeId == expenseTypeId).ToList().Select(s => s.ToDTO(coffeeroomno));

            return Request.CreateResponse(HttpStatusCode.OK, sp);
        }

        [Route(RoutesConstants.RemoveMappedSuplyProductsToExpense)]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveMappedSuplyProductsToExpense([FromUri]int coffeeroomno, [FromUri]int expenseTypeId, [FromUri]int suplyProductId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var sp = entities.SupliedProducts.FirstOrDefault(t =>  t.Id == suplyProductId);
            if (sp != null)
            {
                sp.ExprenseTypeId = null;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route(RoutesConstants.Payment)]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var expense = JsonConvert.DeserializeObject<Models.Expense>(request);

            var entities = new  CoffeeRoomEntities();
            entities.Expenses.Add(DbMapper.Map(expense));
            var suplyProduct =
                entities.SupliedProducts.FirstOrDefault(
                    s => s.ExprenseTypeId.HasValue && s.ExprenseTypeId.Value == expense.ExpenseId);
            if (suplyProduct != null)
            {
                var quantity = entities.SuplyProductQuantities.FirstOrDefault(q =>
                    q.SuplyProductId == suplyProduct.Id && q.CoffeeRoomId == coffeeroomno);
                if (quantity != null)
                {
                    quantity.Quantity += expense.ItemCount;

                }
                else
                {
                    var newQuantity = new SuplyProductQuantity()
                    {
                        CoffeeRoomId = coffeeroomno,
                        SuplyProductId = suplyProduct.Id,
                        Quantity = expense.ItemCount
                    };
                    entities.SuplyProductQuantities.Add(newQuantity);

                }
            }
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId);
            currentShift.TotalExprenses += expense.Amount;
            currentShift.TotalAmount -= expense.Amount;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.AddExpenseExtended)]
        [HttpPost]
        public async Task<HttpResponseMessage> AddExpenseExtended([FromUri]int coffeeroomno, [FromUri]int shiftId, HttpRequestMessage message)
        {
            var request = await message.Content.ReadAsStringAsync();
            var expenseEx = JsonConvert.DeserializeObject<Models.ExpenseType>(request);

            var entities = new CoffeeRoomEntities();
            var expense = new Expense();
            expense.Amount = expenseEx.SuplyProducts.Sum(s => s.Price);
            expense.Quantity = expenseEx.SuplyProducts.Sum(s => (int?)s.Quatity ?? 0);
            expense.ExpenseType = expenseEx.Id;
            expense.CoffeeRoomNo = coffeeroomno;
            expense.ShiftId = shiftId;
            expense.ExpenseSuplyProducts = new List<ExpenseSuplyProduct>();
            foreach (var suplyProduct in expenseEx.SuplyProducts)
            {
                if(!suplyProduct.Quatity.HasValue || suplyProduct.Quatity <=0 || suplyProduct.Price <= 0)
                {
                    continue;
                }
                var expenseDb = new ExpenseSuplyProduct()
                {
                    CoffeeRoonNo = coffeeroomno,
                    Amount = suplyProduct.Price,
                    SuplyProductId = suplyProduct.Id
                };

                expenseDb.Quantity = suplyProduct.Quatity.Value;
                
                expense.ExpenseSuplyProducts.Add(expenseDb);
            }

            entities.Expenses.Add(expense);

            foreach (var suplyProduct in expenseEx.SuplyProducts)
            {
                if (!suplyProduct.Quatity.HasValue || suplyProduct.Quatity <= 0 || suplyProduct.Price <= 0)
                {
                    continue;
                }
                var suplyProductQuantity = entities.SuplyProductQuantities.Include(s => s.SupliedProduct).FirstOrDefault(s => s.SuplyProductId == suplyProduct.Id && s.CoffeeRoomId == coffeeroomno);
    
                if (suplyProductQuantity != null)
                {
                    suplyProductQuantity.Quantity += suplyProduct.Quatity.Value * suplyProductQuantity.SupliedProduct.ExpenseNumerationMultyplier;
                }
                else
                {
                    var sp = entities.SupliedProducts.First(s => s.Id == suplyProduct.Id);
                    var newQuantity = new SuplyProductQuantity()
                    {
                        CoffeeRoomId = coffeeroomno,
                        SuplyProductId = suplyProduct.Id,
                        Quantity = suplyProduct.Quatity.Value * sp.ExpenseNumerationMultyplier
                    };
                    entities.SuplyProductQuantities.Add(newQuantity);
                }            
            }
           
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId);
            currentShift.TotalExprenses += expense.Amount;
            currentShift.TotalAmount -= expense.Amount;
            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetExpenseDetails)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenseDetails([FromUri]int coffeeroomno, [FromUri]int expenseId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var sp = entities.ExpenseSuplyProducts.Include(e => e.SupliedProduct).Where(t => t.CoffeeRoonNo == coffeeroomno && t.ExpenseId == expenseId).ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, sp);
        }

        [Route(RoutesConstants.DeleteExpenseItem)]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteExpense([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var expense = entities.Expenses.First(e => e.Id == id && e.CoffeeRoomNo.Value == coffeeroomno);

            if (expense.IsUserSalaryPayment)
            {
                var user = entities.Users.FirstOrDefault(u => u.Id == expense.UserId.Value);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"No user with ID {expense.UserId} found");
                }
                user.CurrentEarnedAmount += expense.Amount;
                user.EntireEarnedAmount -= expense.Amount;
            }
            else
            {
                var suplyProducts =
                    entities.ExpenseSuplyProducts.Where(p => p.ExpenseId == id && p.CoffeeRoonNo == coffeeroomno);

                foreach (var sp in suplyProducts)
                {
                    var suplyProductQuantity = entities.SuplyProductQuantities.Include(s => s.SupliedProduct).First(s => s.SuplyProductId == sp.SuplyProductId && s.CoffeeRoomId == coffeeroomno);
                    suplyProductQuantity.Quantity -= sp.Quantity * suplyProductQuantity.SupliedProduct.ExpenseNumerationMultyplier;
                    entities.ExpenseSuplyProducts.Remove(sp);
                }
            }
            var currentShift = entities.Shifts.First(s => s.Id == expense.ShiftId);
            currentShift.TotalExprenses -= expense.Amount;
            currentShift.TotalAmount += expense.Amount;

            entities.Expenses.Remove(expense);

            await entities.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.AddNewExpenseType)]
        [HttpPut]
        public async Task<HttpResponseMessage> AddNewExpenseType([FromUri]int coffeeroomno, [FromUri]string typeName)
        {
            var entities = new  CoffeeRoomEntities();
            var type = new ExpenseType() {CoffeeRoomNo = coffeeroomno, Name = typeName};
            entities.ExpenseTypes.Add(type);
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetShiftExpenses)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetShiftExpenses([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var expenses = entities.Expenses.Include("ExpenseType1").Where(e => e.CoffeeRoomNo == coffeeroomno && e.ShiftId.Value == id).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }

        [Route(RoutesConstants.GetExpenses)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetExpenses([FromUri]int coffeeroomno, [FromUri]DateTime from, [FromUri]DateTime to, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var expenses = entities.Expenses.Include(i => i.ExpenseType1).Include(i => i.Shift).Where(e => e.CoffeeRoomNo == coffeeroomno && e.Shift.Date > from && e.Shift.Date < to).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, expenses);
        }

        [Route(RoutesConstants.GetSalesByDate)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetSalesByDate([FromUri]int coffeeroomno, [FromUri]DateTime from, [FromUri]DateTime to, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var sales = entities.Sales.Where(s => s.Time > from && s.Time < to && s.CoffeeRoomNo == coffeeroomno).Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, sales);           
        }

        [Route(RoutesConstants.CashOutCreditCard)]
        [HttpPost]
        public async Task<HttpResponseMessage> CashOutCreditCard([FromUri]int coffeeroomno, decimal amount,  HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var currentShift = entities.Shifts.FirstOrDefault(s => s.CoffeeRoomNo == coffeeroomno && s.IsFinished.Value == false);
            if (currentShift?.TotalCreditCardAmount != null && currentShift.TotalCreditCardAmount.Value >= amount)
            {
                currentShift.TotalCreditCardAmount -= amount;
                currentShift.TotalAmount += amount;

                var cashoutHistory = new CashoutHistory();
                cashoutHistory.CoffeeRoomNo = coffeeroomno;
                cashoutHistory.Amount = amount;
                cashoutHistory.Date = DateTime.Now;
                cashoutHistory.ShiftId = currentShift.Id;
                entities.CashoutHistories.Add(cashoutHistory);

                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.GetCashOutHistory)]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCashOutHistory([FromUri]int coffeeroomno, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var histories = entities.CashoutHistories.Where(s => s.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
        
            return Request.CreateResponse(HttpStatusCode.OK, histories);
        }

        [Route(RoutesConstants.SetCreditCardEntireMoney)]
        [HttpPost]
        public async Task<HttpResponseMessage> SetCreditCardEntireMoney([FromUri]int coffeeroomno, decimal amount, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var shift = entities.Shifts.Where(s => s.CoffeeRoomNo == coffeeroomno).OrderByDescending(s => s.Id).FirstOrDefault();

            if (shift != null)
            {
                shift.TotalCreditCardAmount = amount;
                entities.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

    }
}

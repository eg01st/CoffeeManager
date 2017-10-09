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
            var types = entities.ExpenseTypes.Where(t => !t.IsRemoved).Include(i => i.SupliedProducts)
                .Where(t => t.CoffeeRoomNo == coffeeroomno).ToList().Select(s => s.ToDTO());
            return Request.CreateResponse(HttpStatusCode.OK, types);
        }

        [Route(RoutesConstants.RemoveExpenseType)]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveExpenseType([FromUri]int coffeeroomno, [FromUri] int expenseTypeId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var type = entities.ExpenseTypes.FirstOrDefault(t => t.Id == expenseTypeId && t.CoffeeRoomNo == coffeeroomno);
            type.IsRemoved = true;
            await entities.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleExpenseEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleExpenseEnabled([FromUri]int coffeeroomno, [FromUri]int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var type = entities.ExpenseTypes.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == id);
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
            var sp = entities.SupliedProducts.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == suplyProductId);
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
            var sp = entities.SupliedProducts.Where(t => t.CoffeeRoomNo == coffeeroomno && t.ExprenseTypeId == expenseTypeId).ToList().Select(s => s.ToDTO());

            return Request.CreateResponse(HttpStatusCode.OK, sp);
        }

        [Route(RoutesConstants.RemoveMappedSuplyProductsToExpense)]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveMappedSuplyProductsToExpense([FromUri]int coffeeroomno, [FromUri]int expenseTypeId, [FromUri]int suplyProductId, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var sp = entities.SupliedProducts.FirstOrDefault(t => t.CoffeeRoomNo == coffeeroomno && t.Id == suplyProductId);
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
                if (suplyProduct.Quantity.HasValue)
                {
                    suplyProduct.Quantity += expense.ItemCount;
                }
                else
                {
                    suplyProduct.Quantity = expense.ItemCount;
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
                var suplyProductDb = entities.SupliedProducts.First(s => s.Id == suplyProduct.Id);
    
                if (suplyProductDb.Quantity.HasValue)
                {
                    suplyProductDb.Quantity += suplyProduct.Quatity * suplyProductDb.ExpenseNumerationMultyplier;
                }
                else
                {
                    suplyProductDb.Quantity = suplyProduct.Quatity * suplyProductDb.ExpenseNumerationMultyplier;
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

            var suplyProducts = entities.ExpenseSuplyProducts.Where(p => p.ExpenseId == id && p.CoffeeRoonNo == coffeeroomno);

            foreach (var sp in suplyProducts)
            {
                var suplyProduct = entities.SupliedProducts.First(s => s.Id == sp.SuplyProductId);
                suplyProduct.Quantity -= sp.Quantity * suplyProduct.ExpenseNumerationMultyplier;
                    
                entities.ExpenseSuplyProducts.Remove(sp);
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
            if (currentShift?.TotalCreditCardAmount != null && currentShift.TotalCreditCardAmount.Value > amount)
            {
                currentShift.TotalCreditCardAmount -= amount;
                currentShift.TotalAmount += amount;
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}

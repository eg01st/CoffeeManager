using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Common;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class PaymentServiceProvider : ServiceBase, IPaymentServiceProvider
    {
        public async Task<decimal> GetCurrentShiftMoney()
        {
            var request = CreateGetRequest(RoutesConstants.GetCurrentShiftMoney);
            return await ExecuteRequestAsync<decimal>(request);

          //  return await Get<decimal>(RoutesConstants.GetCurrentShiftMoney, null);
        }

        public async Task<decimal> GetEntireMoney()
        {
            var request = CreateGetRequest(RoutesConstants.GetEntireMoney);
            return await ExecuteRequestAsync<decimal>(request);
           // return await Get<decimal>(RoutesConstants.GetEntireMoney, null);
        }

        public async Task AddExpense(int shiftId, int expenseId, decimal amount, int itemCount)
        {
            var request = CreatePutRequest(RoutesConstants.Payment);
            request.AddBody(new Expense()
            {
                Amount = amount,
                ItemCount = itemCount,
                ExpenseId = expenseId,
                ShiftId = shiftId,
                CoffeeRoomNo = Config.CoffeeRoomNo
            });
            await ExecuteRequestAsync(request);
            //await
            //Put(RoutesConstants.Payment,
                    //new Expense()
                    //{
                    //    Amount = amount,
                    //    ItemCount = itemCount,
                    //    ExpenseId = expenseId,
                    //    ShiftId = shiftId,
                    //    CoffeeRoomNo = Config.CoffeeRoomNo
                    //});
        }

        public async Task<ExpenseType[]> GetExpenseItems()
        {
            var request = CreateGetRequest(RoutesConstants.GetExpenseItems);
            return await ExecuteRequestAsync<ExpenseType[]>(request);
           // return await Get<ExpenseType[]>(RoutesConstants.GetExpenseItems);
        }

        public async Task AddNewExpenseType(string typeName)
        {
            var request = CreatePutRequest(RoutesConstants.AddNewExpenseType);
            request.Parameters.Add(new Parameter(){Name = nameof(typeName), Value = typeName});
            await ExecuteRequestAsync(request);
           // await Put<object>(RoutesConstants.AddNewExpenseType, null, new Dictionary<string, string>() { { nameof(typeName), typeName } });
        }


        public async Task DeleteExpenseItem(int id)
        {
            var request = CreateDeleteRequest(RoutesConstants.DeleteExpenseItem);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);
            //await Delete(RoutesConstants.DeleteExpenseItem, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<Expense[]> GetShiftExpenses(int id)
        {
            var request = CreateGetRequest(RoutesConstants.GetShiftExpenses);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            return await ExecuteRequestAsync<Expense[]>(request);
         //   return await Get<Expense[]>(RoutesConstants.GetShiftExpenses, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task ToggleIsActiveExpense(int id)
        {
            var request = CreatePostRequest(RoutesConstants.ToggleExpenseEnabled);
            request.Parameters.Add(new Parameter() { Name = nameof(id), Value = id });
            await ExecuteRequestAsync(request);

            //await Post<object>(RoutesConstants.ToggleExpenseEnabled, null, new Dictionary<string, string>()
            //{
            //    { nameof(id), id.ToString()}
            //});
        }

        public async Task MapExpenseToSuplyProduct(int expenseTypeId, int suplyProductId)
        {
            var request = CreatePostRequest(RoutesConstants.MapExpenseToSuplyProduct);
            request.Parameters.Add(new Parameter() { Name = nameof(expenseTypeId), Value = expenseTypeId});
            request.Parameters.Add(new Parameter() { Name = nameof(suplyProductId), Value = suplyProductId });
            await ExecuteRequestAsync(request);

            //await Post<object> (RoutesConstants.MapExpenseToSuplyProduct, null, new Dictionary<string, string>()
            //{
            //    { nameof(expenseTypeId), expenseTypeId.ToString()},
            //     { nameof(suplyProductId), suplyProductId.ToString()},
            //});
        }

        public async Task RemoveMappedSuplyProductsToExpense(int expenseTypeId, int suplyProductId)
        {
            var request = CreateDeleteRequest(RoutesConstants.RemoveMappedSuplyProductsToExpense);
            request.Parameters.Add(new Parameter() { Name = nameof(expenseTypeId), Value = expenseTypeId });
            request.Parameters.Add(new Parameter() { Name = nameof(suplyProductId), Value = suplyProductId });
            await ExecuteRequestAsync(request);

            //await Delete(RoutesConstants.RemoveMappedSuplyProductsToExpense, new Dictionary<string, string>()
            //{
            //    { nameof(expenseTypeId), expenseTypeId.ToString()},
            //     { nameof(suplyProductId), suplyProductId.ToString()},
            //});
        }

        public async Task<IEnumerable<SupliedProduct>> GetMappedSuplyProductsToExpense(int expenseTypeId)
        {
            var request = CreateGetRequest(RoutesConstants.GetMappedSuplyProductsToExpense);
            request.Parameters.Add(new Parameter() { Name = nameof(expenseTypeId), Value = expenseTypeId });
            return await ExecuteRequestAsync<IEnumerable<SupliedProduct>>(request);

            //return await Get<IEnumerable<SupliedProduct>>(RoutesConstants.GetMappedSuplyProductsToExpense, new Dictionary<string, string>()
            //{
            //    { nameof(expenseTypeId), expenseTypeId.ToString()},
            //});
        }

        public async Task AddExpense(ExpenseType type, int shiftId)
        {
            var request = CreatePostRequest(RoutesConstants.AddExpenseExtended);
            request.Parameters.Add(new Parameter() { Name = nameof(shiftId), Value = shiftId });
            await ExecuteRequestAsync(request);
            //await Post(RoutesConstants.AddExpenseExtended, type, new Dictionary<string, string>()
            //{
            //    { nameof(shiftId), shiftId.ToString()},
            //});
        }

        public async Task RemoveExpenseType(int expenseTypeId)
        {
            var request = CreateDeleteRequest(RoutesConstants.RemoveExpenseType);
            request.Parameters.Add(new Parameter() { Name = nameof(expenseTypeId), Value = expenseTypeId });
            await ExecuteRequestAsync(request);

            //await Delete(RoutesConstants.RemoveExpenseType, new Dictionary<string, string>()
            //{
            //    { nameof(expenseTypeId), expenseTypeId.ToString()},
            //});
        }

        public async Task<IEnumerable<SupliedProduct>> GetExpenseDetails(int expenseId)
        {
            var request = CreateGetRequest(RoutesConstants.GetExpenseDetails);
            request.Parameters.Add(new Parameter() { Name = nameof(expenseId), Value = expenseId });
            return await ExecuteRequestAsync<IEnumerable<SupliedProduct>>(request);

            //return await Get<IEnumerable<SupliedProduct>>(RoutesConstants.GetExpenseDetails, new Dictionary<string, string>()
            //{
            //    { nameof(expenseId), expenseId.ToString()},
            //});
        }

        public async Task<decimal> GetCreditCardEntireMoney()
        {
            var request = CreateGetRequest(RoutesConstants.GetCreditCardEntireMoney);
            return await ExecuteRequestAsync<decimal>(request);
            //return await Get<decimal>(RoutesConstants.GetCreditCardEntireMoney);
        }

        public async Task CashOutCreditCard(decimal amount)
        {
            var request = CreatePostRequest(RoutesConstants.CashOutCreditCard);
            request.Parameters.Add(new Parameter() { Name = nameof(amount), Value = amount });
            await ExecuteRequestAsync(request);

           // await Post<object>(RoutesConstants.CashOutCreditCard, null, new Dictionary<string, string>() { { nameof(amount), amount.ToString() } });
        }

        public async Task SetCreditCardEntireMoney(decimal amount)
        {
            var request = CreatePostRequest(RoutesConstants.SetCreditCardEntireMoney);
            request.Parameters.Add(new Parameter() { Name = nameof(amount), Value = amount });
            await ExecuteRequestAsync(request);

          //  await Post<object>(RoutesConstants.SetCreditCardEntireMoney, null, new Dictionary<string, string>() { { nameof(amount), amount.ToString() } });
        }

        public async Task<IEnumerable<CashoutHistory>> GetCashoutHistory()
        {
            var request = CreateGetRequest(RoutesConstants.GetCashOutHistory);
            return await ExecuteRequestAsync<IEnumerable<CashoutHistory>>(request);
            //return await Get<IEnumerable<CashoutHistory>>(RoutesConstants.GetCashOutHistory);
        }
    }
}

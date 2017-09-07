using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class PaymentServiceProvider : BaseServiceProvider, IPaymentServiceProvider
    {
        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await Get<decimal>(RoutesConstants.GetCurrentShiftMoney, null);
        }

        public async Task<decimal> GetEntireMoney()
        {
            return await Get<decimal>(RoutesConstants.GetEntireMoney, null);
        }

        public async Task AddExpense(int shiftId, int expenseId, decimal amount, int itemCount)
        {
            await
            Put(RoutesConstants.Payment,
                    new Expense()
                    {
                        Amount = amount,
                        ItemCount = itemCount,
                        ExpenseId = expenseId,
                        ShiftId = shiftId,
                        CoffeeRoomNo = CoffeeRoomNo
                    });
        }

        public async Task<ExpenseType[]> GetExpenseItems()
        {
            return await Get<ExpenseType[]>(RoutesConstants.GetExpenseItems);
        }

        public async Task AddNewExpenseType(string typeName)
        {
            await Put<object>(RoutesConstants.AddNewExpenseType, null, new Dictionary<string, string>() { { nameof(typeName), typeName } });
        }


        public async Task DeleteExpenseItem(int id)
        {
            await Delete(RoutesConstants.DeleteExpenseItem, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task<Expense[]> GetShiftExpenses(int id)
        {
            return await Get<Expense[]>(RoutesConstants.GetShiftExpenses, new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task ToggleIsActiveExpense(int id)
        {
            await Post<object>(RoutesConstants.ToggleExpenseEnabled, null, new Dictionary<string, string>()
            {
                { nameof(id), id.ToString()}
            });
        }

        public async Task MapExpenseToSuplyProduct(int expenseTypeId, int suplyProductId)
        {
            await Post<object> (RoutesConstants.MapExpenseToSuplyProduct, null, new Dictionary<string, string>()
            {
                { nameof(expenseTypeId), expenseTypeId.ToString()},
                 { nameof(suplyProductId), suplyProductId.ToString()},
            });
        }

        public async Task RemoveMappedSuplyProductsToExpense(int expenseTypeId, int suplyProductId)
        {
            await Delete(RoutesConstants.RemoveMappedSuplyProductsToExpense, new Dictionary<string, string>()
            {
                { nameof(expenseTypeId), expenseTypeId.ToString()},
                 { nameof(suplyProductId), suplyProductId.ToString()},
            });
        }

        public async Task<IEnumerable<SupliedProduct>> GetMappedSuplyProductsToExpense(int expenseTypeId)
        {
            return await Get<IEnumerable<SupliedProduct>>(RoutesConstants.GetMappedSuplyProductsToExpense, new Dictionary<string, string>()
            {
                { nameof(expenseTypeId), expenseTypeId.ToString()},
            });
        }
    }
}

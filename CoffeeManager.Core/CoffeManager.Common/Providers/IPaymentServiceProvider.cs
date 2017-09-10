using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IPaymentServiceProvider
    {
        Task<decimal> GetCurrentShiftMoney();

        Task<decimal> GetEntireMoney();

        Task AddExpense(int shiftId, int expenseId, decimal amount, int itemCount);

        Task<ExpenseType[]> GetExpenseItems();

        Task AddNewExpenseType(string typeName);

        Task DeleteExpenseItem(int id);

        Task<Expense[]> GetShiftExpenses(int id);

        Task ToggleIsActiveExpense(int id);

        Task MapExpenseToSuplyProduct(int expenseTypeId, int suplyProductId);
        Task RemoveMappedSuplyProductsToExpense(int expenseTypeId, int suplyProductId);
        Task<IEnumerable<SupliedProduct>> GetMappedSuplyProductsToExpense(int expenseTypeId);

        Task AddExpense(ExpenseType type, int shiftId);

        Task RemoveExpenseType(int expenseTypeId);

        Task<IEnumerable<SupliedProduct>> GetExpenseDetails(int expenseId);
    }
}

using System;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IPaymentManager
    {
        Task<Expense[]> GetShiftExpenses(int id);

        Task<ExpenseType[]> GetExpenseItems();

        Task ToggleIsActiveExpense(int id);

        Task<decimal> GetCurrentShiftMoney();

        Task<decimal> GetEntireMoney();

        Task AddExpense(int expenseId, decimal amout, int itemCount);

        Task<ExpenseType[]> GetActiveExpenseItems();

        Task AddNewExpenseType(string typeName);

        Task<Expense[]> GetShiftExpenses();

        Task DeleteExpenseItem(int id);
    }
}

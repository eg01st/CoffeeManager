using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public interface IPaymentManager
    {
        Task<Expense[]> GetShiftExpenses(int id);

        Task<ExpenseType[]> GetExpenseItems();

        Task ToggleIsActiveExpense(int id);

        Task<decimal> GetCurrentShiftMoney();

        Task<decimal> GetEntireMoney();

        Task<decimal> GetCreditCardEntireMoney();

        Task SetCreditCardEntireMoney(decimal amount);

        Task AddExpense(int expenseId, decimal amout, int itemCount);

        Task<ExpenseType[]> GetActiveExpenseItems();

        Task AddNewExpenseType(string typeName);

        Task<Expense[]> GetShiftExpenses();

        Task DeleteExpenseItem(int id);

        Task MapExpenseToSuplyProduct(int expenseTypeId, int suplyProductId);

        Task RemoveMappedSuplyProductsToExpense(int expenseTypeId, int suplyProductId);

        Task<IEnumerable<SupliedProduct>> GetMappedSuplyProductsToExpense(int expenseTypeId);

        Task AddExpense(ExpenseType type);

        Task RemoveExpenseType(int expenseTypeId);

        Task<IEnumerable<SupliedProduct>> GetExpenseDetails(int expenseId);

        Task CashOutCreditCard(decimal amount);

        Task<IEnumerable<CashoutHistory>> GetCashoutHistory();
    }
}

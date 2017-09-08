﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class PaymentManager : BaseManager, IPaymentManager
    {
        private readonly IPaymentServiceProvider paymentProvider;

        public PaymentManager(IPaymentServiceProvider paymentProvider)
        {
            this.paymentProvider = paymentProvider;
        }

        public async Task<Expense[]> GetShiftExpenses(int id)
        {
            return await paymentProvider.GetShiftExpenses(id);
        }

        public async Task<ExpenseType[]> GetExpenseItems()
        {
            return await paymentProvider.GetExpenseItems();
        }

        public async Task ToggleIsActiveExpense(int id)
        {
            await paymentProvider.ToggleIsActiveExpense(id);
        }

        public async Task<decimal> GetCurrentShiftMoney()
        {
            return await paymentProvider.GetCurrentShiftMoney();
        }

        public async Task<decimal> GetEntireMoney()
        {
            return await paymentProvider.GetEntireMoney();
        }

        public async Task AddExpense(int expenseId, decimal amout, int itemCount)
        {
            await paymentProvider.AddExpense(ShiftNo, expenseId, amout, itemCount);
        }

        public async Task<ExpenseType[]> GetActiveExpenseItems()
        {
            var expenses = await paymentProvider.GetExpenseItems();
            return expenses.Where(e => e.IsActive).ToArray();
        }

        public async Task AddNewExpenseType(string typeName)
        {
            await paymentProvider.AddNewExpenseType(typeName);
        }

        public async Task<Expense[]> GetShiftExpenses()
        {
            return await paymentProvider.GetShiftExpenses(ShiftNo);
        }

        public async Task DeleteExpenseItem(int id)
        {
            await paymentProvider.DeleteExpenseItem(id);
        }

        public async Task MapExpenseToSuplyProduct(int expenseTypeId, int suplyProductId)
        {
            await paymentProvider.MapExpenseToSuplyProduct(expenseTypeId, suplyProductId);
        }

        public async Task RemoveMappedSuplyProductsToExpense(int expenseTypeId, int suplyProductId)
        {
            await paymentProvider.RemoveMappedSuplyProductsToExpense(expenseTypeId, suplyProductId);
        }

        public async Task<IEnumerable<SupliedProduct>> GetMappedSuplyProductsToExpense(int expenseTypeId)
        {
            return await paymentProvider.GetMappedSuplyProductsToExpense(expenseTypeId);
        }

        public async Task AddExpense(ExpenseType type)
        {
            await paymentProvider.AddExpense(type, ShiftNo);
        }
    }
}

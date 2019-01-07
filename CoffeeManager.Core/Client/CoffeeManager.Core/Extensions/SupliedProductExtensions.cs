using System;
using CoffeeManager.Models;
namespace CoffeeManager.Core
{
    public static class SupliedProductExtensions
    {
        public static void SetExpenseNumerationQuantity(this SupliedProduct product)
        {
            var currentQuantity = product.Quatity;
            product.Quatity = currentQuantity * product.ExpenseNumerationMultyplier;
        }
    }
}

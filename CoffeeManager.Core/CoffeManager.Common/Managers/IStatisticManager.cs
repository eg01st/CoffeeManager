using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IStatisticManager
    {
        Task<IEnumerable<Expense>> GetExpenses(DateTime from, DateTime to);

        Task<IEnumerable<SaleInfo>> GetSales(DateTime from, DateTime to);
        Task<IEnumerable<Sale>> GetCreditCardSales(DateTime from, DateTime to);

        Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to);
    }
}

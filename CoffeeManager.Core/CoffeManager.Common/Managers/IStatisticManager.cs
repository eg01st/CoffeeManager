using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public interface IStatisticManager
    {
        Task<IEnumerable<Expense>> GetExpenses(int coffeeRoomId,DateTime from, DateTime to);

        Task<IEnumerable<SaleInfo>> GetSales(int coffeeRoomId,DateTime from, DateTime to);

        Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to);
    }
}

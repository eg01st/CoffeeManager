using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class StatisticManager : BaseManager, IStatisticManager
    {
        private readonly IStatisticProvider provider;

        public StatisticManager(IStatisticProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<Expense>> GetExpenses(int coffeeRoomId, DateTime from, DateTime to)
        {
            return await provider.GetExpenses(coffeeRoomId, from, to);
        }

        public async Task<IEnumerable<SaleInfo>> GetSales(int coffeeRoomId, DateTime from, DateTime to)
        {
            return await provider.GetSales(coffeeRoomId, from, to);
        }

        public async Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to)
        {
            return await provider.GetSalesByNames(itemsNames, from, to);
        }
    }
}

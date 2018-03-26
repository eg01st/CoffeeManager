using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Managers
{
    public class StatisticManager : BaseManager, IStatisticManager
    {
        private readonly IStatisticProvider provider;

        public StatisticManager(IStatisticProvider provider)
        {
            this.provider = provider;
        }

        public async Task<IEnumerable<Expense>> GetExpenses(DateTime from, DateTime to)
        {
            return await provider.GetExpenses(from, to);
        }

        public async Task<IEnumerable<SaleInfo>> GetSales(DateTime from, DateTime to)
        {
            return await provider.GetSales(from, to);
        }

        public async Task<IEnumerable<Sale>> GetCreditCardSales(DateTime from, DateTime to)
        {
            return await provider.GetCreditCardSales(from, to);
        }

        public async Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to)
        {
            return await provider.GetSalesByNames(itemsNames, from, to);
        }
    }
}

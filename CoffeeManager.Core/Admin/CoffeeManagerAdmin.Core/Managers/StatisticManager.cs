using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;

namespace CoffeeManagerAdmin.Core.Managers
{
    public class StatisticManager : BaseManager
    {
        StatisticProvider provider = new StatisticProvider();

        public StatisticManager()
        {
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

        public async  Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to)
        {
            return await provider.GetSalesByNames(itemsNames,from , to);
        }
    }
}

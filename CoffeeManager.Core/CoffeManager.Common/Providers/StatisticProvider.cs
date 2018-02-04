using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class StatisticProvider : BaseServiceProvider, IStatisticProvider
    {
        public async Task<IEnumerable<Expense>> GetExpenses(DateTime from, DateTime to)
        {
            return await Get<Expense[]>(RoutesConstants.StatisticGetExpenses, new Dictionary<string, string>()
            {
                { nameof(from), from.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz")},
                { nameof(to), to.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz")}
            });
        }

        public async Task<IEnumerable<SaleInfo>> GetSales(DateTime from, DateTime to)
        {
            return await Get<SaleInfo[]>(RoutesConstants.StatisticGetAllSales, new Dictionary<string, string>()
            {
                { nameof(from), from.ToString()},
                { nameof(to), to.ToString()}
            });
        }

        public async Task<IEnumerable<Sale>> GetCreditCardSales(DateTime from, object to)
        {
            return await Get<Sale[]>(RoutesConstants.StatisticGetCreditCardSales, new Dictionary<string, string>()
            {
                { nameof(from), from.ToString()},
                { nameof(to), to.ToString()}
            });
        }

        public async Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to)
        {
                return await Post<Sale[], IEnumerable<string>>(RoutesConstants.StatisticGetSalesByName, itemsNames, new Dictionary<string, string>()
            {
                { nameof(from), from.ToString()},
                { nameof(to), to.ToString()}
            });
        }
    }
}

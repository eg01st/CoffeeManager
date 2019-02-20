using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common.Providers
{
    public class StatisticProvider : BaseServiceProvider, IStatisticProvider
    {
        public async Task<IEnumerable<Expense>> GetExpenses(int coffeeRoomId, DateTime from, DateTime to)
        {
            return await Get<Expense[]>(RoutesConstants.StatisticGetExpenses, new Dictionary<string, string>()
            {
                { nameof(from), from.ToString()},
                { nameof(to), to.ToString()}
//                { nameof(from), from.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz")},
//                { nameof(to), to.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz")}
            }, coffeeRoomId);
        }

        public async Task<IEnumerable<SaleInfo>> GetSales(int coffeeRoomId, DateTime from, DateTime to)
        {
            return await Get<SaleInfo[]>(RoutesConstants.StatisticGetAllSales, new Dictionary<string, string>()
            {
                { nameof(from), from.ToString()},
                { nameof(to), to.ToString()}
            }, coffeeRoomId);
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

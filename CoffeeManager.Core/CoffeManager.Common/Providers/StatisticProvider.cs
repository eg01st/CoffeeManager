using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class StatisticProvider : ServiceBase, IStatisticProvider
    {
        public async Task<IEnumerable<Expense>> GetExpenses(DateTime from, DateTime to)
        {
            var request = CreateGetRequest(RoutesConstants.StatisticGetExpenses);
            request.Parameters.Add(new Parameter() { Name = nameof(from), Value = from });
            request.Parameters.Add(new Parameter() { Name = nameof(to), Value = to });
            return await ExecuteRequestAsync<Expense[]>(request);
        }

        public async Task<IEnumerable<SaleInfo>> GetSales(DateTime from, DateTime to)
        {
            var request = CreateGetRequest(RoutesConstants.StatisticGetAllSales);
            request.Parameters.Add(new Parameter() { Name = nameof(from), Value = from });
            request.Parameters.Add(new Parameter() { Name = nameof(to), Value = to });
            return await ExecuteRequestAsync<IEnumerable<SaleInfo>>(request);
        }

        public async Task<IEnumerable<Sale>> GetCreditCardSales(DateTime from, object to)
        {
            var request = CreateGetRequest(RoutesConstants.StatisticGetCreditCardSales);
            request.Parameters.Add(new Parameter() { Name = nameof(from), Value = from });
            request.Parameters.Add(new Parameter() { Name = nameof(to), Value = to });
            return await ExecuteRequestAsync<IEnumerable<Sale>>(request);
        }

        public async Task<IEnumerable<Sale>> GetSalesByNames(IEnumerable<string> itemsNames, DateTime from, DateTime to)
        {
            var request = CreatePostRequest(RoutesConstants.StatisticGetSalesByName);
            request.Parameters.Add(new Parameter() { Name = nameof(from), Value = from });
            request.Parameters.Add(new Parameter() { Name = nameof(to), Value = to });
            request.AddBody(itemsNames);
            return await ExecuteRequestAsync<IEnumerable<Sale>>(request);
        }
    }
}

using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeManager.Common;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core
{
    public class CreditCardSalesViewModel : ViewModelBase
    {
        public List<StatisticSaleItemViewModel> Sales {get;set;}
        public decimal EntireSaleAmount {get;set;}

        readonly IStatisticManager manager;
        readonly DateTime from;
        readonly DateTime to;

        public CreditCardSalesViewModel(IStatisticManager manager, DateTime from, DateTime to)
        {
            this.to = to;
            this.from = from;
            this.manager = manager;
        }

        public async Task Init()
        {
            IEnumerable<Sale> sales = await manager.GetCreditCardSales(from, to);
            Sales = sales.Select(s => new StatisticSaleItemViewModel(s)).ToList();
            EntireSaleAmount = sales.Sum(s => s.Amount);
            RaiseAllPropertiesChanged();
        }
    }
}

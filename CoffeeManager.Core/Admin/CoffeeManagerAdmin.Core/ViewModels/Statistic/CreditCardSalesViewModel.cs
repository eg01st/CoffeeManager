using System;
using System.Collections;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class CreditCardSalesViewModel : ViewModelBase
    {
        public List<StatisticSaleItemViewModel> Sales {get;set;}
        public decimal EntireSaleAmount {get;set;}

        public CreditCardSalesViewModel()
        {
        }

        public void Init(Guid id)
        {
            IEnumerable<Sale> sales;
            ParameterTransmitter.TryGetParameter(id, out sales);
            Sales = sales.Select(s => new StatisticSaleItemViewModel(s)).ToList();
            EntireSaleAmount = sales.Sum(s => s.Amount);
            RaiseAllPropertiesChanged();
        }
    }
}

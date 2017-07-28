using System;
using System.Collections;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core
{
    public class CreditCardSalesViewModel : ViewModelBase
    {
        public List<SaleItemViewModel> Sales {get;set;}
        public decimal EntireSaleAmount {get;set;}

        public CreditCardSalesViewModel()
        {
        }

        public void Init(Guid id)
        {
            IEnumerable<Sale> sales;
            ParameterTransmitter.TryGetParameter(id, out sales);
            Sales = sales.Select(s => new SaleItemViewModel(s)).ToList();
            EntireSaleAmount = sales.Sum(s => s.Amount);
            RaiseAllPropertiesChanged();
        }
    }
}

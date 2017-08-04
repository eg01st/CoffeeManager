using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class SalesChartViewModel : ViewModelBase
    {

        public List<Sale> Sales {get;set;}

        private readonly IStatisticManager manager;

        public SalesChartViewModel(IStatisticManager manager)
        {
            this.manager = manager;
        }

        public async void Init(Guid id, DateTime from, DateTime to)
        {
            IEnumerable<string> itemsNames;
            ParameterTransmitter.TryGetParameter(id, out itemsNames);
            var sales = await manager.GetSalesByNames(itemsNames, from, to);  
            Sales = sales.ToList();
            RaisePropertyChanged(nameof(Sales));         
        }
    }
}

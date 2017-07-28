using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core
{
    public class SalesChartViewModel : ViewModelBase
    {
        StatisticManager sm = new StatisticManager();

        public List<Sale> Sales {get;set;}

        public SalesChartViewModel()
        {

        }

        public async void Init(Guid id, DateTime from, DateTime to)
        {
            IEnumerable<string> itemsNames;
            ParameterTransmitter.TryGetParameter(id, out itemsNames);
            var sales = await sm.GetSalesByNames(itemsNames, from, to);  
            Sales = sales.ToList();
            RaisePropertyChanged(nameof(Sales));         
        }
    }
}

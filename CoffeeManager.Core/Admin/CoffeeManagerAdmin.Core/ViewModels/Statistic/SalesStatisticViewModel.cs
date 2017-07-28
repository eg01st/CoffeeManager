using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.Util;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class SalesStatisticViewModel : ViewModelBase
    {
        public IEnumerable<SaleItemViewModel> Items { get { return items;} set { items = value; RaisePropertyChanged(nameof(Items));}}
        IEnumerable<SaleItemViewModel> items;
        IEnumerable<SaleInfo> saleItems;
        DateTime from, to;

        public ICommand ShowChartCommand {get;set;}

        public SalesStatisticViewModel()
        {
            ShowChartCommand = new MvxCommand(DoShowChart);
        }

        public void Init(Guid id, DateTime from, DateTime to)
        {     
            this.from = from;
            this.to = to;
            ParameterTransmitter.TryGetParameter(id, out saleItems);
            Items = saleItems.Select(s=> new SaleItemViewModel(s));
        }

        private void DoShowChart()
        {
            var id = ParameterTransmitter.PutParameter(saleItems);
            ShowViewModel<SelectSalesViewModel>(new {id, from, to});
        }
    }
}

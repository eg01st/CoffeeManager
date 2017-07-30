using System;
using System.Collections.Generic;
using CoffeeManager.Models;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class SalesStatisticViewModel : ViewModelBase
    {
        private IEnumerable<SaleInfo> saleItems;
        DateTime from, to;

        public List<BaseStatisticSaleItemViewModel> Items {get;set;}
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
            
            var entireAmount = saleItems.Sum(i => i.Amount);
            var entireAmountHeaderVm = new StatisticSaleHeaderViewModel("Общая сумма", entireAmount.Value);
            Items = new List<BaseStatisticSaleItemViewModel>();
            Items.Add(entireAmountHeaderVm);
            
            var groupedByProductType = saleItems.GroupBy(g => g.Producttype);
            foreach (var item in groupedByProductType)
            {
                var name = TypesLists.ProductTypesList.First(t => t.Id == item.Key).Name;
                var sum = item.Sum(s => s.Amount);
                var amountByProductType = new StatisticSaleHeaderViewModel(name, sum.Value);
                Items.Add(amountByProductType);
                Items.AddRange(item.Select(s=> new StatisticSaleItemViewModel(s)));
            }
            RaisePropertyChanged(nameof(Items));
        }

        private void DoShowChart()
        {
            var id = ParameterTransmitter.PutParameter(saleItems);
            ShowViewModel<SelectSalesViewModel>(new {id, from, to});
        }
    }
}

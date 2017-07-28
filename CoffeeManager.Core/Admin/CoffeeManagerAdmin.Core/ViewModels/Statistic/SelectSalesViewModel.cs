using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class SelectSalesViewModel : ViewModelBase
    {
        public List<SelectSaleItemViewModel> Items { get { return items;} set { items = value; RaisePropertyChanged(nameof(Items));}}
        List<SelectSaleItemViewModel> items;
        IEnumerable<SaleInfo> saleItems;
        DateTime from, to;
        public SelectSalesViewModel()
        {
            ShowChartCommand = new MvxCommand(DoShowChart);
        }

        public ICommand ShowChartCommand {get;set;}

        public void Init(Guid id, DateTime from, DateTime to)
        {
            this.from = from;
            this.to = to;
            ParameterTransmitter.TryGetParameter(id, out saleItems);
            var groupedSales = saleItems.GroupBy(g => g.Name).Select(s => new SelectSaleItemViewModel(s.Key));
            Items = groupedSales.ToList();
        }

        private void DoShowChart()
        {
            var selectedItems = Items.Where(i => i.IsSelected).Select(s => s.Name);
            var id = ParameterTransmitter.PutParameter(selectedItems);
            ShowViewModel<SalesChartViewModel>(new {id, from, to});
        }
    }
}

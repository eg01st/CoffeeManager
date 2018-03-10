using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class SelectSalesViewModel : BaseSearchViewModel<SelectSaleItemViewModel>
    {
        Guid id;
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
            this.id = id;
        }

        private void DoShowChart()
        {
            var selectedItems = Items.Where(i => i.IsSelected).Select(s => s.Name);
            var paramId = ParameterTransmitter.PutParameter(selectedItems);
            ShowViewModel<SalesChartViewModel>(new {id = paramId, from, to});
        }

        public override Task<List<SelectSaleItemViewModel>> LoadData()
        {
            ParameterTransmitter.TryGetParameter(id, out saleItems);
            var items = saleItems.GroupBy(g => g.Name).Select(s => new SelectSaleItemViewModel(s.Key)).ToList();
            return Task.FromResult(items);
        }
    }
}

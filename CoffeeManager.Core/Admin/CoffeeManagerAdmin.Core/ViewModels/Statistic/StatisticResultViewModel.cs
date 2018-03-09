using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.Util;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class StatisticResultViewModel : ViewModelBase
    {
        private readonly IStatisticManager manager;

        public ExpensesStatisticViewModel ExpensesVm { get; set; }
        public CreditCardSalesViewModel CreditCardSalesVm { get; set; }
        public SalesStatisticViewModel SalesVm { get; set; }

        public ICommand ShowChartCommand { get; set; }

        public StatisticResultViewModel(IStatisticManager manager)
        {
            this.manager = manager;
            ShowChartCommand = new MvxCommand(DoShowChart);
        }

        public async Task Init(DateTime from, DateTime to)
        {
            await ExecuteSafe(async () =>
            {
                ExpensesVm = new ExpensesStatisticViewModel(manager, from, to);
                CreditCardSalesVm = new CreditCardSalesViewModel(manager, from, to);
                SalesVm = new SalesStatisticViewModel(manager, from, to);
                var tasks = new[]
                {
                    ExpensesVm.Init(),
                    CreditCardSalesVm.Init(),
                    SalesVm.Init()

                };
                await Task.WhenAll(tasks);
                RaiseAllPropertiesChanged();
           });
        }

        private void DoShowChart()
        {
          //  var id = ParameterTransmitter.PutParameter(SalesVm.Items);
           // ShowViewModel<SelectSalesViewModel>(new { id, from, to });
        }
    }
}

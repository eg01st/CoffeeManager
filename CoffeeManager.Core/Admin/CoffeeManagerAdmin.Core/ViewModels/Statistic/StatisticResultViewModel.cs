using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class StatisticResultViewModel : ViewModelBase, IMvxViewModel<Tuple<DateTime, DateTime>>
    {
        private DateTime from, to;
        
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

        public async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                ExpensesVm = new ExpensesStatisticViewModel(manager, from, to);
                CreditCardSalesVm = new CreditCardSalesViewModel(manager, from, to);
                SalesVm = new SalesStatisticViewModel(manager, from, to);
                var tasks = new[]
                {
                    ExpensesVm.Initialize(),
                    CreditCardSalesVm.Initialize(),
                    SalesVm.Initialize()

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

        public void Prepare(Tuple<DateTime, DateTime> parameter)
        {
            @from = parameter.Item1;
            to = parameter.Item2;
        }
    }
}

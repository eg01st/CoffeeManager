using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.NavigationArgs;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class StatisticResultSubViewModel : PageViewModel, IMvxViewModel<ChildStatisticNavigationArgs>
    {
        private DateTime from, to;
        private int coffeeRoomId;
        private bool isAlreadyAppeared;

        private readonly IStatisticManager manager;

        public ExpensesStatisticViewModel ExpensesVm { get; set; }
        public SalesStatisticViewModel SalesVm { get; set; }


        private readonly ICategoryManager categoryManager;

        public StatisticResultSubViewModel(IStatisticManager manager, ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
            this.manager = manager;
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            if (!isAlreadyAppeared)
            {
                isAlreadyAppeared = true;
                await LoadDataAsync();
            }
        }
        
        private async Task LoadDataAsync()
        {
            await ExecuteSafe(async () =>
            {
                ExpensesVm = new ExpensesStatisticViewModel(manager, from, to, coffeeRoomId);
                SalesVm = new SalesStatisticViewModel(manager, categoryManager, from, to, coffeeRoomId);
                var tasks = new[]
                {
                    ExpensesVm.Initialize(),
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

        public void Prepare(ChildStatisticNavigationArgs parameter)
        {
            @from = parameter.From;
            to = parameter.To;
            coffeeRoomId = parameter.CoffeeRoomId;
        }
    }
}

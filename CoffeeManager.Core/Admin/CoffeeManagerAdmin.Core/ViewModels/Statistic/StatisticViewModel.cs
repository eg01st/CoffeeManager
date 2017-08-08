using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManagerAdmin.Core.Util;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core
{
    public class StatisticViewModel : ViewModelBase
    {
        private readonly IStatisticManager manager;

        private DateTime from = DateTime.Now.Date.AddMonths(-1);
        private DateTime to = DateTime.Now.Date;
    

        public ICommand ShowExpensesCommand { get; set; }
        public ICommand ShowSalesCommand { get; set; }
        public ICommand ShowCreditCardInfoCommand { get; set; }
        public ICommand GetDataCommand { get; set; }

        public DateTime From { get { return from; }  set { from = value;  RaisePropertyChanged(nameof(From));} }
        public DateTime To { get { return to; } set { to = value; RaisePropertyChanged(nameof(To)); } }



        public StatisticViewModel(IStatisticManager manager)
        {
            this.manager = manager;
            ShowExpensesCommand = new MvxCommand(DoShowExpenses);
            ShowSalesCommand = new MvxCommand(DoShowSales);
            ShowCreditCardInfoCommand = new MvxCommand(DoShowCreditCardInfo);
            GetDataCommand = new MvxCommand(DoGetData);
        }

        public async void DoShowCreditCardInfo()
        {
            await ExecuteSafe(async () =>
            {
			    var sales = await manager.GetCreditCardSales(From, To);
			    var id = ParameterTransmitter.PutParameter(sales);
			    ShowViewModel<CreditCardSalesViewModel>(new { id = id });
            });
        }

        private async void DoShowSales()
        {
            await ExecuteSafe(async () =>
            {
			    var sales = await manager.GetSales(From, To);
			    var id = ParameterTransmitter.PutParameter(sales);
			    ShowViewModel<SalesStatisticViewModel>(new { id = id, from, to = To });
            });
        }

        private async void DoShowExpenses()
        {
            await ExecuteSafe(async () =>
            {
                var expenses = await manager.GetExpenses(From, To);
                var id = ParameterTransmitter.PutParameter(expenses);
                ShowViewModel<ExpensesStatisticViewModel>(new { id = id });
            });
        }

        private void DoGetData()
        {
            UserDialogs.ActionSheet(new Acr.UserDialogs.ActionSheetConfig()
            {
                Options = new List<ActionSheetOption>()
                {
                    new ActionSheetOption("Траты", () => DoShowExpenses()),
                    new ActionSheetOption("Продажи", () => DoShowSales()),
                    new ActionSheetOption("Продажи по кредитке", () => DoShowCreditCardInfo()),
                }
            });
        }
    }
}

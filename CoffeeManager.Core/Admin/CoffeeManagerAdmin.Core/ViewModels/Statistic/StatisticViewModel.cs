using System;
using System.Collections.Generic;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManagerAdmin.Core.Managers;
using CoffeeManagerAdmin.Core.Util;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class StatisticViewModel : ViewModelBase
    {
        StatisticManager sm = new StatisticManager();

        private DateTime from = DateTime.Now.Date.AddMonths(-1);
        private DateTime to = DateTime.Now.Date;
    

        public ICommand ShowExpensesCommand { get; set; }
        public ICommand ShowSalesCommand { get; set; }
        public ICommand ShowCreditCardInfoCommand { get; set; }
        public ICommand GetDataCommand { get; set; }

        public DateTime From { get { return from; }  set { from = value;  RaisePropertyChanged(nameof(From));} }
        public DateTime To { get { return to; } set { to = value; RaisePropertyChanged(nameof(To)); } }

        public StatisticViewModel()
        {
            ShowExpensesCommand = new MvxCommand(DoShowExpenses);
            ShowSalesCommand = new MvxCommand(DoShowSales);
            ShowCreditCardInfoCommand = new MvxCommand(DoShowCreditCardInfo);
            GetDataCommand = new MvxCommand(DoGetData);
        }

        public async void DoShowCreditCardInfo()
        {
            var toDate = To.AddDays(1);
            var sales = await sm.GetCreditCardSales(From, toDate);
            var id = ParameterTransmitter.PutParameter(sales);
            ShowViewModel<CreditCardSalesViewModel>(new {id = id});
        }

        private async void DoShowSales()
        {
            var toDate = To.AddDays(1);
            var sales = await sm.GetSales(From, toDate);
            var id = ParameterTransmitter.PutParameter(sales);
            ShowViewModel<SalesStatisticViewModel>(new {id = id, from, to = toDate});
        }

        private async void DoShowExpenses()
        {
            var toDate = To.AddDays(1);
            var expenses = await sm.GetExpenses(From, toDate);
            var id = ParameterTransmitter.PutParameter(expenses);
            ShowViewModel<ExpensesStatisticViewModel>(new { id = id });
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

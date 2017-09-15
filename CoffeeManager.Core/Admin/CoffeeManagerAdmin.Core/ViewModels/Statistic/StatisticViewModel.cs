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
    
        public ICommand GetDataCommand { get; set; }

        public DateTime From { get { return from; }  set { from = value;  RaisePropertyChanged(nameof(From));} }
        public DateTime To { get { return to; } set { to = value; RaisePropertyChanged(nameof(To)); } }

        public StatisticViewModel(IStatisticManager manager)
        {
            this.manager = manager;
            GetDataCommand = new MvxCommand(DoGetData);
        }

        private void DoGetData()
        {
            ShowViewModel<StatisticResultViewModel>(new { from = From, to = To});
        }
    }
}

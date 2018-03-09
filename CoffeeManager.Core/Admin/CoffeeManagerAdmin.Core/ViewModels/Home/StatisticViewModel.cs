﻿using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class StatisticViewModel : ViewModelBase
    {

        public string Name { get; set; } = "StatisticViewModel";

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
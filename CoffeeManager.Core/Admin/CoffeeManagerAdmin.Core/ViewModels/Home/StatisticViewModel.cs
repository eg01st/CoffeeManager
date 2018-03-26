using System;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class StatisticViewModel : ViewModelBase
    {

        private readonly IStatisticManager manager;

        private DateTime from = DateTime.Now.Date.AddMonths(-1);
        private DateTime to = DateTime.Now.Date;
    
        public ICommand GetDataCommand { get; set; }

        public DateTime From 
        { 
            get => from;
            set { from = value;  RaisePropertyChanged(nameof(From));}
            
        }
        public DateTime To 
        { 
            get => to;
            set { to = value; RaisePropertyChanged(nameof(To)); } }

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

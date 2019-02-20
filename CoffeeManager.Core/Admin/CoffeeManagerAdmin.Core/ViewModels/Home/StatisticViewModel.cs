using System;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.NavigationArgs;
using CoffeeManagerAdmin.Core.ViewModels.Statistic;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;

namespace CoffeeManagerAdmin.Core.ViewModels.Home
{
    public class StatisticViewModel : AdminPageViewModel
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

        private async void DoGetData()
        {
            await NavigationService.Navigate<StatisticResultParentViewModel, StatisticNavigationArgs>(new StatisticNavigationArgs(From, To, CoffeeRooms));
        }
    }
}

using System;
using System.Collections.Generic;
using CoffeeManagerAdmin.Core.NavigationArgs;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core.ViewModels.Statistic
{
    public class StatisticResultParentViewModel : AdminPageViewModel, IMvxViewModel<StatisticNavigationArgs>
    {
        private StatisticNavigationArgs args;
        
        private IList<string> titles = new List<string>();
        
        public IList<string> Titles
        {
            get { return titles; }
            set
            {
                titles = value;
                RaisePropertyChanged();
            }
        }

        public void ShowInitialsViewModels()
        {
            foreach (var cr in args.CoffeeRooms)
            {
                var title = cr.Name;
                Titles.Add(title);
                var childArgs = new ChildStatisticNavigationArgs()
                {
                    From = args.From,
                    To = args.To,
                    CoffeeRoomId = cr.Id
                };
                NavigationService.Navigate<StatisticResultSubViewModel, ChildStatisticNavigationArgs>(childArgs);
            }
        }

        public void Prepare(StatisticNavigationArgs parameter)
        {
            args = parameter;
        }
    }
}
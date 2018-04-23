using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common.ViewModels;
using MobileCore.Extensions;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Users
{
    public class UserEarningsViewModel : ViewModelBase, IMvxViewModel<UserEarningsHistory[]>
    {
        public List<UserEarningItemViewModel> Items { get; set; }

        public MvxAsyncCommand<UserEarningItemViewModel> ItemSelectedCommand { get; }
        
        public UserEarningsViewModel()
        {
            ItemSelectedCommand = new MvxAsyncCommand<UserEarningItemViewModel>(OnItemSelectedAsync);
        }
        
        private async Task OnItemSelectedAsync(UserEarningItemViewModel item)
        {
            item.ThrowIfNull(nameof(item));
            
            item.SelectCommand.Execute();

            await Task.Yield();
        }

        public void Prepare(UserEarningsHistory[] parameter)
        {
            Items = parameter?.Select(s => new UserEarningItemViewModel(s)).ToList();
            RaisePropertyChanged(nameof(Items));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Users
{
    public class UserEarningsViewModel : ViewModelBase
    {
        public List<UserEarningItemViewModel> Items { get; set; }

        public void Init(Guid id)
        {
            UserEarningsHistory[] items;
            ParameterTransmitter.TryGetParameter(id, out items);
            Items = items?.Select(s => new UserEarningItemViewModel(s)).ToList();
            RaisePropertyChanged(nameof(Items));
        }
    }
}

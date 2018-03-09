using System;
using System.Collections.Generic;
using CoffeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Util;
using System.Linq;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
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

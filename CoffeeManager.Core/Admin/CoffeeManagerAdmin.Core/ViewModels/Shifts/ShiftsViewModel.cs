using System;
using System.Collections.Generic;
using System.Linq;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ShiftsViewModel : ViewModelBase
    {
        private readonly IShiftManager manager;
        
        private List<ShiftItemViewModel> _items = new List<ShiftItemViewModel>();
        public List<ShiftItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }


        public ShiftsViewModel(IShiftManager manager)
        {
            this.manager = manager;
        }

        public async void Init()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetShifts();
                if (items != null)
                {
                    Items = items.Select(s => new ShiftItemViewModel(s)).OrderByDescending(o => o.Id).ToList();
                }
                else
                {
                    UserDialogs.Alert("Empty list from server");
                }
            });
        }
    }
}

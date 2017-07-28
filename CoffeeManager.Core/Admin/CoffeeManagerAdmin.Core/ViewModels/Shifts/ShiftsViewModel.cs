using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.Managers;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ShiftsViewModel : ViewModelBase
    {
        private ShiftManager manager = new ShiftManager();
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

        public async void Init()
        {
            try
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
            }
            catch (Exception ex)
            {
                UserDialogs.Alert(ex.ToString());
            }
        }
    }
}

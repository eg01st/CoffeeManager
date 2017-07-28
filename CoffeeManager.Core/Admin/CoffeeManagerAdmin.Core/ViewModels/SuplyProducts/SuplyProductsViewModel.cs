using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SuplyProductsViewModel : ViewModelBase
    {
        private MvxSubscriptionToken _listChanged;

        private SuplyProductsManager manager = new SuplyProductsManager();
        private List<SuplyProductItemViewModel> _items;

        public List<SuplyProductItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public SuplyProductsViewModel()
        {
            _listChanged = Subscribe<SuplyListChangedMessage>(async (obj) => await LoadList());
        }

        public async void Init()
        {
            try
            {
                await LoadList();
            }
            catch (Exception ex)
            {
                UserDialogs.Alert(ex.ToString());
            }
        }

        private async Task LoadList()
        {
            var items = await manager.GetSupliedProducts();
            Items = items.Select(s => new SuplyProductItemViewModel(s)).ToList();
        }

    }
}

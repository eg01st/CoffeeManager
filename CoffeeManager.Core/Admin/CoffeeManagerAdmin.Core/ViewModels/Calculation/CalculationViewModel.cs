using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class CalculationViewModel : ViewModelBase
    {
        private MvxSubscriptionToken _listChangedToken;
        SuplyProductsManager suplyProductsManager = new SuplyProductsManager();
        private string _name;
        private List<CalculationItemViewModel> _items;
        private ICommand _addItemCommand;
        private int _productId;

        public CalculationViewModel()
        {
            _addItemCommand = new MvxCommand(DoAddItem);
            _listChangedToken = Subscribe<CalculationListChangedMessage>(async (obj) => await LoadData());
        }

        private void DoAddItem()
        {
            ShowViewModel<SelectCalculationListViewModel>(new { productId = _productId });
        }

        public async void Init(int id)
        {
            _productId = id;
            await LoadData();
        }

        private async Task LoadData()
        {
            var info = await suplyProductsManager.GetProductCalculationItems(_productId);
            _productId = info.ProductId;
            Name = info.Name;
            Items = info.SuplyProductInfo.Select(s => new CalculationItemViewModel(s)).ToList();
        }

        public ICommand AddItemCommand => _addItemCommand;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public List<CalculationItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
    }
}

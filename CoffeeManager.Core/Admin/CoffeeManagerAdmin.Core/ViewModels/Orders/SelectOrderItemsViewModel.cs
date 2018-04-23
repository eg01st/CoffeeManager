using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Orders
{
    public class SelectOrderItemsViewModel : ViewModelBase, IMvxViewModel<int>
    {
        private string _newProductName;
        private int _orderId;

        private string _searchString;

        private MvxSubscriptionToken _token;
        readonly ISuplyProductsManager manager;
        readonly ISuplyOrderManager orderManager;

        public SelectOrderItemsViewModel(ISuplyProductsManager manager, ISuplyOrderManager orderManager)
        {
            this.orderManager = orderManager;
            this.manager = manager;
            DoneCommand = new MvxCommand(DoDoneCommand);
            AddNewSuplyProductCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AddSuplyProductViewModel>());
            _token = Subscribe<SuplyListChangedMessage>(async (obj) => await LoadData());
        }

        public override async Task Initialize()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var items = await manager.GetSuplyProducts();
            _orginalItems = Items = items.Select(s => new SelectOrderItemViewModel(manager, orderManager, _orderId, s)).ToList();
        }

        private void DoDoneCommand()
        {
            Publish(new OrderItemsListChangedMessage(this));
            Close(this);
        }


        private List<SelectOrderItemViewModel> _orginalItems;
        private List<SelectOrderItemViewModel> _items;

        public List<SelectOrderItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public string NewProductName
        {
            get { return _newProductName; }
            set
            {
                _newProductName = value;
                RaisePropertyChanged(nameof(NewProductName));
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                RaisePropertyChanged(nameof(SearchString));
                if (!string.IsNullOrWhiteSpace(SearchString))
                {
                    Items = Items.Where(i => i.Name.StartsWith(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    Items = _orginalItems;
                }
            }
        }

        public ICommand DoneCommand { get; set; }

        public ICommand AddNewSuplyProductCommand { get; }

        protected override void DoUnsubscribe()
        {
            Unsubscribe<SuplyListChangedMessage>(_token);
        }

        public void Prepare(int parameter)
        {
            _orderId = parameter;
        }
    }
}

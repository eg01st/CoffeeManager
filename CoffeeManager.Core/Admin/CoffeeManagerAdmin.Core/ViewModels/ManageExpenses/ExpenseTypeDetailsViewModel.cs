using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core
{
    public class ExpenseTypeDetailsViewModel : ViewModelBase
    {
        private int expenseTypeId;
        readonly IPaymentManager paymentManager;
        private readonly MvxSubscriptionToken reloadDataToken;

        public ICommand MapSuplyProductCommand { get; }

        public string ExpenseName { get; set; }

        public List<MappedSuplyProductItemViewModel> MappedItems { get; set; }

        public ExpenseTypeDetailsViewModel(IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            MapSuplyProductCommand = new MvxCommand(DoMapSuplyProduct);
            reloadDataToken = Subscribe<MappedSuplyProductChangedMessage>(async (obj) => await LoadMappedSuplyProducts());
        }

        private void DoMapSuplyProduct()
        {
            ShowViewModel<MapExpenseToSuplyProductViewModel>(new {id = expenseTypeId});
        }

        public async void Init(Guid id)
        {
            ExpenseType type;
            Util.ParameterTransmitter.TryGetParameter(id, out type);
            expenseTypeId = type.Id;
            ExpenseName = type.Name;
            RaisePropertyChanged(nameof(ExpenseName));
            await LoadMappedSuplyProducts();
        }

        private async Task LoadMappedSuplyProducts()
        {
            await ExecuteSafe(async () =>
            {
                var items = await paymentManager.GetMappedSuplyProductsToExpense(expenseTypeId);
                MappedItems = items.Select(s => new MappedSuplyProductItemViewModel(s, expenseTypeId)).ToList();
                RaisePropertyChanged(nameof(MappedItems));
            });
        }
    }
}

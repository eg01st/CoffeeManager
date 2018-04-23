using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.ManageExpenses
{
    public class ExpenseTypeDetailsViewModel : ViewModelBase, IMvxViewModel<ExpenseType>
    {
        ExpenseType type;
        private int expenseTypeId;
        readonly IPaymentManager paymentManager;
        private readonly MvxSubscriptionToken reloadDataToken;

        public ICommand MapSuplyProductCommand { get; }

        public string ExpenseName { get; set; }

        public List<MappedSuplyProductItemViewModel> MappedItems { get; set; }

        public ExpenseTypeDetailsViewModel(IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            MapSuplyProductCommand = new MvxAsyncCommand(DoMapSuplyProduct);
            reloadDataToken = Subscribe<MappedSuplyProductChangedMessage>(async (obj) => await LoadMappedSuplyProducts());
        }

        private async Task DoMapSuplyProduct()
        {
           await NavigationService.Navigate<MapExpenseToSuplyProductViewModel, int>(expenseTypeId);
        }

        public override async Task Initialize()
        {
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

        protected override void DoUnsubscribe()
        {
            Unsubscribe<MappedSuplyProductChangedMessage>(reloadDataToken);
        }

        public void Prepare(ExpenseType parameter)
        {
            type = parameter;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private readonly IPaymentManager manager;

        protected List<ExpenseItemExtendedViewModel> _items;
        protected List<ExpenseItemExtendedViewModel> _searchItems;
        private ExpenseItemExtendedViewModel _selectedExpence;
        private string _searchString;

        public ExpenseItemExtendedViewModel SelectedExpense
        {
            get { return _selectedExpence; }
            set
            {
                _selectedExpence = value;
                RaisePropertyChanged(nameof(SelectedExpense));
                RaisePropertyChanged(nameof(IsAddButtomEnabled));
            }
        }

        public List<ExpenseItemExtendedViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public List<ExpenseItemExtendedViewModel> SearchItems
        {
            get { return string.IsNullOrEmpty(SearchString) ? _items : _searchItems; }
            set
            {
                _searchItems = value;
                RaisePropertyChanged(nameof(SearchItems));
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                RaisePropertyChanged(nameof(SearchString));
                SearchCommand.Execute(null);
            }
        }
  

        public ICommand AddExpenseCommand { get; }
        public ICommand SearchCommand { get; }


        public bool IsAddButtomEnabled =>  SelectedExpense != null;

        public ExpenseViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            AddExpenseCommand = new MvxCommand(DoAddExpense);
            SearchCommand = new MvxCommand(DoSearch);
        }

        private void DoSearch()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                SearchItems = Items.Where(i => i.Name.ToUpper().StartsWith(SearchString.ToUpper())).ToList();
            }
            else
            {
                SearchItems = Items;
            }
        }

        private void DoAddExpense()
        {
            if(SelectedExpense.ExpenseSuplyProducts.Count < 1
               || SelectedExpense.ExpenseSuplyProducts.All(e => e.Amount <= 0 && e.ItemCount <= 0))
            {
                Alert("Не вписаны детали поставки");
                return;
            }

            if (SelectedExpense.ExpenseSuplyProducts.Any(e => (e.Amount > 0 && e.ItemCount <=0) || (e.Amount <= 0 && e.ItemCount > 0)))
            {
                Alert("Неверно вписаны детали поставки");
                return;
            }

            var sum = SelectedExpense.ExpenseSuplyProducts.Sum(s => s.Amount);
            Confirm($"Добавить сумму {sum} как трату за {SelectedExpense.Name}?", () => AddExpense());
        }

        private async void AddExpense()
        {
            var type = new ExpenseType();
            type.Id = SelectedExpense.Id;
            type.CoffeeRoomNo = SelectedExpense.CoffeeRoomNo;
            type.SuplyProducts = SelectedExpense.ExpenseSuplyProducts.Where(e => e.Amount > 0 && e.ItemCount > 0)
                .Select(s => new SupliedProduct()
                {
                    Id = s.Id,
                    Price = s.Amount,
                    Quatity = s.ItemCount,
                    CoffeeRoomNo = SelectedExpense.CoffeeRoomNo
                })
                .ToArray();
            await manager.AddExpense(type);
            Close(this);
        }


        public async void Init()
        {
            await LoadTypes();
        }

        private async Task LoadTypes()
        {
            await ExecuteSafe(async () =>
            {
                var result = await manager.GetActiveExpenseItems();
                SearchItems = Items = result.Select(s => new ExpenseItemExtendedViewModel(s)).ToList();
            });
        }
    }
}

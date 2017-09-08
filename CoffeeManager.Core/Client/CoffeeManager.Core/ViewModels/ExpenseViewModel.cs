﻿using System.Collections.Generic;
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
        private string _amount;
        private string _itemCount;


        public ExpenseItemExtendedViewModel SelectedExpense
        {
            get { return _selectedExpence; }
            set
            {
                _selectedExpence = value;
                RaisePropertyChanged(nameof(SelectedExpense));
                RaisePropertyChanged(nameof(IsSimpleExpense));
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

        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                RaisePropertyChanged(nameof(Amount));
                RaisePropertyChanged(nameof(IsAddButtomEnabled));
            }
        }

        public string ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                RaisePropertyChanged(nameof(ItemCount));
                RaisePropertyChanged(nameof(IsAddButtomEnabled));
            }
        }


        public ICommand AddExpenseCommand { get; }
        public ICommand SearchCommand { get; }


        public bool IsAddButtomEnabled => IsSimpleExpense ? (!string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(ItemCount)) :  SelectedExpense != null;

        public bool IsSimpleExpense => SelectedExpense?.ExpenseSuplyProducts == null || SelectedExpense?.ExpenseSuplyProducts?.Count < 1;

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
            if (IsSimpleExpense)
            {
                Confirm($"Добавить сумму {Amount} как трату за {SelectedExpense.Name}?", () => AddExpense());
            }
            else
            {
                if (SelectedExpense.ExpenseSuplyProducts.Count < 1
                   || SelectedExpense.ExpenseSuplyProducts.All(e => e.Amount <= 0 && e.ItemCount <= 0))
                {
                    Alert("Не вписаны детали поставки");
                    return;
                }

                if (SelectedExpense.ExpenseSuplyProducts.Any(e => (e.Amount > 0 && e.ItemCount <= 0) || (e.Amount <= 0 && e.ItemCount > 0)))
                {
                    Alert("Неверно вписаны детали поставки");
                    return;
                }

                var sum = SelectedExpense.ExpenseSuplyProducts.Sum(s => s.Amount);
                Confirm($"Добавить сумму {sum} как трату за {SelectedExpense.Name}?", () => AddExpense());
            }
        }

        private async void AddExpense()
        {
            if (IsSimpleExpense)
            {
                await manager.AddExpense(SelectedExpense.Id, decimal.Parse(Amount), int.Parse(ItemCount));
            }
            else
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
            }
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

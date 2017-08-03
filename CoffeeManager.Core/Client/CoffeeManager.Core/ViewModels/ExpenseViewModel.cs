using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private PaymentManager _paymentManager = new PaymentManager();

        protected List<BaseItemViewModel> _items;
        protected List<BaseItemViewModel> _searchItems;
        private BaseItemViewModel _selectedExpence;
        private string _searchString;
        private string _amount;
        private string _itemCount;
        private string _newExpenseType;
        private ICommand _addNewExprenseTypeCommand;
        private ICommand _addExpenseCommand;
        private ICommand _selectExpenseTypeCommand;
        private ICommand _seachCommand;

        public List<BaseItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public List<BaseItemViewModel> SearchItems
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

        public string SelectedExprense
        {
            get { return _selectedExpence.Name; }
            set
            {
                _selectedExpence.Name = value;
                RaisePropertyChanged(nameof(SelectedExprense));
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
        public string NewExprenseType
        {
            get { return _newExpenseType; }
            set
            {
                _newExpenseType = value;
                RaisePropertyChanged(nameof(NewExprenseType));
                RaisePropertyChanged(nameof(IsAddNewExpenseTypeEnabled));
            }
        }

        public ICommand AddNewExprenseTypeCommand => _addNewExprenseTypeCommand;
        public ICommand AddExpenseCommand => _addExpenseCommand;
        public ICommand SelectExpenseTypeCommand => _selectExpenseTypeCommand;
        public ICommand SearchCommand => _seachCommand;

        public ICommand ShowCurrentShiftExpensesCommand { get; set; }

        public bool IsAddNewExpenseTypeEnabled => !string.IsNullOrEmpty(NewExprenseType);
        public bool IsAddButtomEnabled => !string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(SelectedExprense) && !string.IsNullOrEmpty(ItemCount);


        public ExpenseViewModel()
        {
            _addNewExprenseTypeCommand = new MvxCommand(DoAddNewExpenseType);
            _addExpenseCommand = new MvxCommand(DoAddExpense);
            _selectExpenseTypeCommand = new MvxCommand<BaseItemViewModel>(DoSelectExpenseType);
            ShowCurrentShiftExpensesCommand = new MvxCommand(DoShowExpenses);
            _seachCommand = new MvxCommand(DoSearch);
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

        private void DoShowExpenses()
        {
            ShowViewModel<CurrentShiftExpensesViewModel>();
        }

        private void DoSelectExpenseType(BaseItemViewModel item)
        {
            _selectedExpence = item;
            RaisePropertyChanged(nameof(SelectedExprense));
            RaisePropertyChanged(nameof(IsAddButtomEnabled));
        }

        private void DoAddExpense()
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = $"Добавить сумму {Amount} как трату за {SelectedExprense}?",
                OnAction = async (ok) =>
                {
                    if (ok)
                    {
                        var amount = decimal.Parse(Amount);
                        await _paymentManager.AddExpense(_selectedExpence.Id, amount, int.Parse(ItemCount));
                        Publish(new ExpenseAddedMessage(amount, this));
                        Close(this);
                    }
                }
            });
        }

        private void DoAddNewExpenseType()
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = $"Добавить {NewExprenseType} как новый тип расходов?",
                OnAction = async (ok) =>
                {
                    if (ok)
                    {
                        await _paymentManager.AddNewExpenseType(NewExprenseType);
                        LoadTypes();
                        ShowSuccessMessage($"{NewExprenseType} добавлен и находится в списке.");
                        NewExprenseType = string.Empty;
                    }
                }
            });


        }

        public void Init()
        {
            LoadTypes();
        }

        private async void LoadTypes()
        {
            try
            {
                var result = await _paymentManager.GetExpenseItems();
                SearchItems = Items = result.Select(s => new BaseItemViewModel(s)).ToList();
            }
            catch (ArgumentNullException ex)
            {
                Close(this);
            }

        }
    }
}

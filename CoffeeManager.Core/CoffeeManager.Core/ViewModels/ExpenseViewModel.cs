using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ExpenseViewModel : ViewModelBase
    {
        private PaymentManager _paymentManager = new PaymentManager();

        protected List<BaseItemViewModel> _items;
        private BaseItemViewModel _selectedExpence;
        private string _amount;
        private string _newExpenseType;
        private ICommand _addNewExprenseTypeCommand;
        private ICommand _addExpenseCommand;
        private ICommand _selectExpenseTypeCommand;

        public List<BaseItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
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

        public bool IsAddNewExpenseTypeEnabled => !string.IsNullOrEmpty(NewExprenseType);
        public bool IsAddButtomEnabled => !string.IsNullOrEmpty(Amount) && !string.IsNullOrEmpty(SelectedExprense);
        

        public ExpenseViewModel()
        {
             _addNewExprenseTypeCommand = new MvxCommand(DoAddNewExpenseType);
            _addExpenseCommand = new MvxCommand(DoAddExpense);
            _selectExpenseTypeCommand = new MvxCommand<BaseItemViewModel>(DoSelectExpenseType);
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
                OnAction = (ok) =>
                {
                    if (ok)
                    {
                        _paymentManager.AddExpense(_selectedExpence.Id, float.Parse(Amount));
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
                OnAction = (ok) =>
                {
                    if (ok)
                    {
                        _paymentManager.AddNewExpenseType(NewExprenseType);
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

        private void LoadTypes()
        {
            Items = _paymentManager.GetExpenseItems().Select(s => new BaseItemViewModel(s)).ToList();
        }
    }
}

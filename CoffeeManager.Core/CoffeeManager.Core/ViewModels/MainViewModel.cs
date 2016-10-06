using System;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int _userId;
        private int _shiftId;

        private PaymentManager _paymentManager = new PaymentManager();
        private ShiftManager _shiftManager = new ShiftManager();
        private readonly MvxSubscriptionToken token;
        private readonly MvxSubscriptionToken expenseAddedToken;
        private readonly MvxSubscriptionToken deptAddedToken;
        private ICommand _endShiftCommand;
        private ICommand _deleteCupCommand;
        private ICommand _showDeptsCommand;
        private ICommand _showCurrentSalesCommand;
        private ICommand _showExpenseCommand;
        private ICommand _enablePoliceSaleCommand;

        private decimal _currentShiftMoney;
        private decimal _entireMoney;
        private bool _policeSaleEnabled;

        public string CurrentShiftMoney
        {
            get { return _currentShiftMoney.ToString(); }
            set
            {
                _currentShiftMoney = decimal.Parse(value);
                RaisePropertyChanged(nameof(CurrentShiftMoney));
            }
        }

        public string EntireMoney
        {
            get { return _entireMoney.ToString(); }
            set
            {
                _entireMoney = decimal.Parse(value);
                RaisePropertyChanged(nameof(EntireMoney));
            }
        }

        public bool IsPoliceSaleEnabled
        {
            get { return _policeSaleEnabled; }
            set
            {
                _policeSaleEnabled = value;
                RaisePropertyChanged(nameof(IsPoliceSaleEnabled));
            }
        }

        public ICommand EndShiftCommand => _endShiftCommand;
        public ICommand DeleteCupCommand => _deleteCupCommand;
        public ICommand ShowDeptsCommand => _showDeptsCommand;
        public ICommand ShowCurrentSalesCommand => _showCurrentSalesCommand;
        public ICommand ShowExpenseCommand => _showExpenseCommand;
        public ICommand EnablePoliceSaleCommand => _enablePoliceSaleCommand;

        public MainViewModel()
        {
            token = Subscribe<AmoutChangedMessage>(OnCallBackMessage);
            expenseAddedToken = Subscribe<ExpenseAddedMessage>(OnExpenseAdded);
            deptAddedToken = Subscribe<DeptAddedMessage>(OnDeptAdded);
            _endShiftCommand = new MvxCommand(DoEndShift);
            _deleteCupCommand = new MvxCommand(DoShowDeleteCup);
            _showDeptsCommand = new MvxCommand(DoShowDepts);
            _showCurrentSalesCommand = new MvxCommand(DoShowCurrentSales);
            _showExpenseCommand = new MvxCommand(DoShowExpense);
            _enablePoliceSaleCommand = new MvxCommand(DoEnablePoliceSale);
        }

        private void OnDeptAdded(DeptAddedMessage obj)
        {
            if (obj.Data.Item2)
            {
                _entireMoney += obj.Data.Item1;
            }
            else
            {
                _entireMoney -= obj.Data.Item1;
            }
            RaisePropertyChanged(nameof(EntireMoney));
        }

        private void OnExpenseAdded(ExpenseAddedMessage obj)
        {
            _entireMoney -= obj.Data;
            RaisePropertyChanged(nameof(EntireMoney));
        }

        private void DoEnablePoliceSale()
        {
            IsPoliceSaleEnabled = !IsPoliceSaleEnabled;
            Publish(new IsPoliceSaleMessage(_policeSaleEnabled, this));
        }

        private void DoShowExpense()
        {
            ShowViewModel<ExpenseViewModel>();
        }

        private void DoShowCurrentSales()
        {
            ShowViewModel<CurrentShiftSalesViewModel>();
        }

        private void DoShowDepts()
        {
            ShowViewModel<DeptViewModel>();
        }

        private void DoShowDeleteCup()
        {
            ShowViewModel<DeleteCupViewModel>();
        }

        public async void Init(int userId, int shiftId)
        {
            _userId = userId;
            _shiftId = shiftId;

            await _paymentManager.GetCurrentShiftMoney().ContinueWith((amount) =>
            {
                _currentShiftMoney = amount.Result;
                RaisePropertyChanged(nameof(CurrentShiftMoney));
            });

            await _paymentManager.GetEntireMoney().ContinueWith((amount) =>
            {
                _entireMoney = amount.Result;
                RaisePropertyChanged(nameof(EntireMoney));
            });

        }

        private void DoEndShift()
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = "Завершить смену?",
                OnAction =
                            async (confirm) =>
                            {
                                if (confirm)
                                {
                                    await _shiftManager.EndUserShift(_shiftId);
                                    Close(this);

                                }
                            }
            });
        }

        private void OnCallBackMessage(AmoutChangedMessage message)
        {
            if (message.Data.Item2)
            {
                _currentShiftMoney += message.Data.Item1;
                _entireMoney += message.Data.Item1;
            }
            else
            {
                _currentShiftMoney -= message.Data.Item1;
                _entireMoney -= message.Data.Item1;

            }
            RaisePropertyChanged(nameof(CurrentShiftMoney));
            RaisePropertyChanged(nameof(EntireMoney));
        }
    }
}

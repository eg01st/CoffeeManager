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
        private ICommand _endShiftCommand;
        private ICommand _deleteCupCommand;
        private ICommand _showDeptsCommand;
        private ICommand _showCurrentSalesCommand;
        private ICommand _showExpenseCommand;
        private ICommand _enablePoliceSaleCommand;

        private float _currentShiftMoney;
        private float _entireMoney;
        private bool _policeSaleEnabled;

        public string CurrentShiftMoney
        {
            get { return _currentShiftMoney.ToString(); }
            set
            {
                _currentShiftMoney = float.Parse(value);
                RaisePropertyChanged(nameof(CurrentShiftMoney));
            }
        }

        public string EntireMoney
        {
            get { return _entireMoney.ToString(); }
            set
            {
                _entireMoney = float.Parse(value);
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
            _endShiftCommand = new MvxCommand(DoEndShift);
            _deleteCupCommand = new MvxCommand(DoShowDeleteCup);
            _showDeptsCommand = new MvxCommand(DoShowDepts);
            _showCurrentSalesCommand = new MvxCommand(DoShowCurrentSales);
            _showExpenseCommand = new MvxCommand(DoShowExpense);
            _enablePoliceSaleCommand = new MvxCommand(DoEnablePoliceSale);
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

        public void Init(int userId, int shiftId)
        {
            _userId = userId;
            _shiftId = shiftId;

            _currentShiftMoney = _paymentManager.GetCurrentShiftMoney();
            _entireMoney = _paymentManager.GetEntireMoney();
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
                                    ShowViewModel<LoginViewModel>();
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

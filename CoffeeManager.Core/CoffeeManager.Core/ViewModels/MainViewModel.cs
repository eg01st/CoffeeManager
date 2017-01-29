using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int _userId;
        private int _shiftId;

        private MvxSubscriptionToken _productSelectedToken;
        private ICommand _endShiftCommand;
        private ICommand _deleteCupCommand;
        private ICommand _showDeptsCommand;
        private ICommand _showCurrentSalesCommand;
        private ICommand _showExpenseCommand;
        private ICommand _enablePoliceSaleCommand;
        private ICommand _showErrorsCommand;
        private ICommand _payCommand;
        private ICommand _enableCreditCardSaleCommand;


        private bool _policeSaleEnabled;
        private ObservableCollection<ProductViewModel> _selectedProducts = new ObservableCollection<ProductViewModel>();
        private ICommand _itemSelectedCommand;

        public bool IsPoliceSaleEnabled
        {
            get { return _policeSaleEnabled; }
            set
            {
                _policeSaleEnabled = value;
                RaisePropertyChanged(nameof(IsPoliceSaleEnabled));
            }
        }

        public bool IsCreditCardSaleEnabled
        {
            get { return _isCreditCardSaleEnabled; }
            set
            {
                _isCreditCardSaleEnabled = value;
                RaisePropertyChanged(nameof(IsCreditCardSaleEnabled));
            }
        }

        private int _sum;
        public int Sum
        {
            get { return _sum; }
            set
            {
                _sum = value;
                RaisePropertyChanged(nameof(Sum));
            }
        }

        private string _sumButtonText;
        private bool _isCreditCardSaleEnabled;


        public string SumButtonText
        {
            get { return $"Оплатить {Sum} Грн"; }
            set
            {
                _sumButtonText = value;
                RaisePropertyChanged(nameof(SumButtonText));
            }
        }


        public bool PayEnabled => SelectedProducts.Count > 0;

        public ICommand EndShiftCommand => _endShiftCommand;
        public ICommand DeleteCupCommand => _deleteCupCommand;
        public ICommand ShowDeptsCommand => _showDeptsCommand;
        public ICommand ShowCurrentSalesCommand => _showCurrentSalesCommand;
        public ICommand ShowExpenseCommand => _showExpenseCommand;
        public ICommand EnablePoliceSaleCommand => _enablePoliceSaleCommand;
        public ICommand EnableCreditCardSaleCommand => _enableCreditCardSaleCommand;

        public ICommand ShowErrorsCommand => _showErrorsCommand;
        public ICommand PayCommand => _payCommand;
        public ICommand ItemSelectedCommand => _itemSelectedCommand;


        public ObservableCollection<ProductViewModel> SelectedProducts
        {
            get { return _selectedProducts; }
            set
            {
                _selectedProducts = value;
                RaisePropertyChanged(nameof(SelectedProducts));
            }
        }

        public MainViewModel()
        {
            _productSelectedToken = Subscribe<ProductSelectedMessage>(OnProductSelected);
            _endShiftCommand = new MvxCommand(DoEndShift);
            _deleteCupCommand = new MvxCommand(DoShowDeleteCup);
            _showDeptsCommand = new MvxCommand(DoShowDepts);
            _showCurrentSalesCommand = new MvxCommand(DoShowCurrentSales);
            _showExpenseCommand = new MvxCommand(DoShowExpense);
            _enablePoliceSaleCommand = new MvxCommand(DoEnablePoliceSale);
            _showErrorsCommand = new MvxCommand(DoShowErrors);
            _payCommand = new MvxCommand(DoPay);
            _itemSelectedCommand = new MvxCommand<ProductViewModel>(DoSelectItem);
            _enableCreditCardSaleCommand = new MvxCommand(DoEnableCreditCardPay);
        }

        private void DoEnableCreditCardPay()
        {
            IsCreditCardSaleEnabled = !IsCreditCardSaleEnabled;
            Publish(new IsCreditCardSaleMessage(_isCreditCardSaleEnabled, this));
        }

        private void DoSelectItem(ProductViewModel obj)
        {
            SelectedProducts.Remove(obj);
            Sum -= (int)obj.Price;
            RaisePropertyChanged(nameof(PayEnabled));
            RaisePropertyChanged(nameof(SumButtonText));
        }

        private void OnProductSelected(ProductSelectedMessage obj)
        {
            var sender = (ProductViewModel)obj.Sender;
            var prod = sender.Clone();
            prod.IsSelected = true;
            SelectedProducts.Add(prod);
            Sum += (int)prod.Price;
            RaisePropertyChanged(nameof(PayEnabled));
            RaisePropertyChanged(nameof(SumButtonText));
        }

       

        private async void DoPay()
        {
            foreach (var productViewModel in SelectedProducts)
            {
                await ProductManager.SaleProduct(productViewModel.Id, productViewModel.Price, productViewModel.IsPoliceSale, productViewModel.IsCreditCardSale);
            }
            SelectedProducts = new ObservableCollection<ProductViewModel>();
            Sum = 0;
            RaisePropertyChanged(nameof(PayEnabled));
            RaisePropertyChanged(nameof(SumButtonText));
  
        }

        private void DoShowErrors()
        {
            ShowViewModel<ErrorsViewModel>();
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
        }

        private void DoEndShift()
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = "Завершить смену?",
                OnAction =
                            (confirm) =>    
                            {
                                if (confirm)
                                {
                                    ShowViewModel<EndShiftViewModel>(new { shiftId = _shiftId});
                                    Close(this);
                                }
                            }
            });
        }

        public void HandleError(string toString)
        {
            RequestExecutor.LogError(toString);
        }
    }
}

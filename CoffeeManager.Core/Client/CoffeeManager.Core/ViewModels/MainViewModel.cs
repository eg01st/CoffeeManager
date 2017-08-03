﻿﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using CoffeeManager.Core.ServiceProviders;
using MvvmCross.Platform;
using System;
using System.Linq;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        ProductManager prodManager = new ProductManager();
            
        private int _userId;
        private int _shiftId;
        private bool _connectionExists = true;

        private Timer lostConnectionTimer;

        private MvxSubscriptionToken _lostConnectionToken;

        private ICommand _endShiftCommand;
        private ICommand _showDeptsCommand;
        private ICommand _showCurrentSalesCommand;
        private ICommand _showExpenseCommand;
        private ICommand _enablePoliceSaleCommand;
        private ICommand _showErrorsCommand;
        private ICommand _payCommand;
        private ICommand _enableCreditCardSaleCommand;


        private bool _policeSaleEnabled;
        private ObservableCollection<SelectedProductViewModel> _selectedProducts = new ObservableCollection<SelectedProductViewModel>();
        private ICommand _itemSelectedCommand;

        public bool IsPoliceSaleEnabled
        {
            get { return _policeSaleEnabled; }
            set
            {
                _policeSaleEnabled = value;
                RaisePropertyChanged(nameof(IsPoliceSaleEnabled));
                foreach (var item in allProducts)
                {
                    item.IsPoliceSale = _policeSaleEnabled;
                }
            }
        }

        public bool IsCreditCardSaleEnabled
        {
            get { return _isCreditCardSaleEnabled; }
            set
            {
                _isCreditCardSaleEnabled = value;
                RaisePropertyChanged(nameof(IsCreditCardSaleEnabled));
                foreach (var item in allProducts)
                {
                    item.IsCreditCardSale = _isCreditCardSaleEnabled;
                }
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
        public ICommand ShowDeptsCommand => _showDeptsCommand;
        public ICommand ShowCurrentSalesCommand => _showCurrentSalesCommand;
        public ICommand ShowExpenseCommand => _showExpenseCommand;
        public ICommand EnablePoliceSaleCommand => _enablePoliceSaleCommand;
        public ICommand EnableCreditCardSaleCommand => _enableCreditCardSaleCommand;

        public ICommand ShowErrorsCommand => _showErrorsCommand;
        public ICommand PayCommand => _payCommand;
        public ICommand ItemSelectedCommand => _itemSelectedCommand;


        public ObservableCollection<SelectedProductViewModel> SelectedProducts
        {
            get { return _selectedProducts; }
            set
            {
                _selectedProducts = value;
                RaisePropertyChanged(nameof(SelectedProducts));
            }
        }

        public CoffeeViewModel CoffeeProducts {get;}
        public TeaViewModel TeaProducts {get;}
        public SweetsViewModel SweetsProducts {get;}
        public WaterViewModel WaterProducts {get;}
        public AddsViewModel AddsProducts {get;}
        public MealsViewModel MealsProducts {get;}
        public ColdDrinksViewModel ColdDrinksProducts {get;}
        public IceCreamViewModel IceCreamProducts {get;}

        private IEnumerable<ProductItemViewModel> allProducts;

        public MainViewModel(IMvxViewModelLoader mvxViewModelLoader)
        {
            var request = new MvxViewModelRequest(typeof(CoffeeViewModel), null, null, MvxRequestedBy.Unknown);
            CoffeeProducts = (CoffeeViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(TeaViewModel), null, null, MvxRequestedBy.Unknown);
            TeaProducts = (TeaViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(SweetsViewModel), null, null, MvxRequestedBy.Unknown);
            SweetsProducts = (SweetsViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(WaterViewModel), null, null, MvxRequestedBy.Unknown);
            WaterProducts = (WaterViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(AddsViewModel), null, null, MvxRequestedBy.Unknown);
            AddsProducts = (AddsViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(MealsViewModel), null, null, MvxRequestedBy.Unknown);
            MealsProducts = (MealsViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(ColdDrinksViewModel), null, null, MvxRequestedBy.Unknown);
            ColdDrinksProducts = (ColdDrinksViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(IceCreamViewModel), null, null, MvxRequestedBy.Unknown);
            IceCreamProducts = (IceCreamViewModel)mvxViewModelLoader.LoadViewModel(request, null);

           
            _lostConnectionToken = Subscribe<LostConnectionMessage>(OnLostConnection);
            _endShiftCommand = new MvxCommand(DoEndShift);
            _showDeptsCommand = new MvxCommand(DoShowDepts);
            _showCurrentSalesCommand = new MvxCommand(DoShowCurrentSales);
            _showExpenseCommand = new MvxCommand(DoShowExpense);
            _enablePoliceSaleCommand = new MvxCommand(DoEnablePoliceSale);
            _showErrorsCommand = new MvxCommand(DoShowErrors);
            _payCommand = new MvxCommand(DoPay);
            _itemSelectedCommand = new MvxCommand<SelectedProductViewModel>(DoSelectItem);
            _enableCreditCardSaleCommand = new MvxCommand(DoEnableCreditCardPay);
        }

        public async void Init(int userId, int shiftId)
        {
            _userId = userId;
            _shiftId = shiftId;
            await ExecuteSafe( async () => 
            {
                var tasks = new List<Task>();
                tasks.Add(CoffeeProducts.InitViewModel());
                tasks.Add(TeaProducts.InitViewModel());
                tasks.Add(SweetsProducts.InitViewModel());
                tasks.Add(WaterProducts.InitViewModel());
                tasks.Add(AddsProducts.InitViewModel());
                tasks.Add(MealsProducts.InitViewModel());
                tasks.Add(ColdDrinksProducts.InitViewModel());
                tasks.Add(IceCreamProducts.InitViewModel());
                await Task.WhenAll(tasks);
                allProducts = CoffeeProducts.Items
                                            .Concat(TeaProducts.Items)
                                            .Concat(SweetsProducts.Items)
                                            .Concat(WaterProducts.Items)
                                            .Concat(AddsProducts.Items)
                                            .Concat(MealsProducts.Items)
                                            .Concat(ColdDrinksProducts.Items)
                                            .Concat(IceCreamProducts.Items);
    
                SubscribeToSelectProduct();
            });
        }

        private void SubscribeToSelectProduct()
        {
            foreach (var item in allProducts)
            {
                item.ProductSelected += OnProductSelected;
            }
        }

        private void OnProductSelected(object sender, SaleItemEventArgs e)
        {
            ProductItemViewModel prod = (ProductItemViewModel)sender;
            var selectedItem = new SelectedProductViewModel(
                                                        prod.Id,
                                                        prod.Name,
                                                        e.Price,
                                                        prod.IsPoliceSale,
                                                        prod.IsCreditCardSale,
                                                        e.IsSaleByWeight,
                                                        e.Weight);
            SelectedProducts.Add(selectedItem);
            Sum += (int)e.Price;
            RaisePropertyChanged(nameof(PayEnabled));
            RaisePropertyChanged(nameof(SumButtonText));
        }

        #region lost connection

        private void OnLostConnection(LostConnectionMessage obj)
        {
            Alert("Соединение с сервером прерванно, доступна только запись продаж");
            _connectionExists = false;

            lostConnectionTimer = new Timer(HandleTimerCallback, null, 0, 30000);
        }

        private async void HandleTimerCallback(object state)
        {
            bool _success;
            try
            {
                _success = BaseServiceProvider.Ping();
            }
            catch
            {
                return;
            }
            if (_success)
            {
                lostConnectionTimer?.Dispose();
                lostConnectionTimer = null;

                Alert("Соединение с сервером востановленно, доступна обычная работа");
                _connectionExists = true;
            }
        }

        #endregion

        private void DoEnableCreditCardPay()
        {
            IsCreditCardSaleEnabled = !IsCreditCardSaleEnabled;
        }

        private void DoEnablePoliceSale()
        {
            IsPoliceSaleEnabled = !IsPoliceSaleEnabled;
        }

        private void DoSelectItem(SelectedProductViewModel obj)
        {
            SelectedProducts.Remove(obj);
            Sum -= (int)obj.Price;
            RaisePropertyChanged(nameof(PayEnabled));
            RaisePropertyChanged(nameof(SumButtonText));
        }

        private void DoPay()
        {
            var tasks = new List<Task>();
            foreach (var productViewModel in SelectedProducts)
            {
                var task = new Task(async () => await prodManager.SaleProduct(
                    productViewModel.ProductId,
                    productViewModel.Price,
                    productViewModel.IsPoliceSale,
                    productViewModel.IsCreditCardSale,
                    productViewModel.IsSaleByWeight,
                    productViewModel.Weight));
                tasks.Add(task);
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());
            SelectedProducts.Clear();
            Sum = 0;
            RaisePropertyChanged(nameof(PayEnabled));
            RaisePropertyChanged(nameof(SumButtonText));

        }

        private void DoShowErrors()
        {
            ShowViewModel<ErrorsViewModel>();
        }



        private void DoShowExpense()
        {
            if (!_connectionExists)
            {
                Alert("Соединение с сервером прерванно, доступна только запись продаж");
                return;
            }
            ShowViewModel<ExpenseViewModel>();
        }

        private void DoShowCurrentSales()
        {
            if (!_connectionExists)
            {
                Alert("Соединение с сервером прерванно, доступна только запись продаж");
                return;
            }
            ShowViewModel<CurrentShiftSalesViewModel>();
        }

        private void DoShowDepts()
        {
            if (!_connectionExists)
            {
                Alert("Соединение с сервером прерванно, доступна только запись продаж");
                return;
            }
            ShowViewModel<DeptViewModel>();
        }


        private void DoEndShift()
        {
            if (!_connectionExists)
            {
                Alert("Соединение с сервером прерванно, доступна только запись продаж");
                return;
            }
            Confirm("Завершить смену?", () => 
            {
                ShowViewModel<EndShiftViewModel>(new { shiftId = _shiftId });
                Close(this);
            });
        }

        public void HandleError(string toString)
        {
            RequestExecutor.LogError(toString);
        }
    }
}

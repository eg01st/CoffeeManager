﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Linq;
using System.Text;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IProductManager productManager;

        private int _userId;
        private int _shiftId;

        private bool _policeSaleEnabled;
        private bool _isCreditCardSaleEnabled;
        private string _sumButtonText;
        private int _sum;
        private ObservableCollection<SelectedProductViewModel> _selectedProducts = new ObservableCollection<SelectedProductViewModel>();

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


        public int Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                RaisePropertyChanged(nameof(Sum));
                RaisePropertyChanged(nameof(PayEnabled));
                RaisePropertyChanged(nameof(SumButtonText));
            }
        }



        public string SumButtonText
        {
            get
            {
                var sb = new StringBuilder("Оплатить ");
                sb.Append(Sum);
                sb.Append(" Грн");
                return sb.ToString();
            }
            set
            {
                _sumButtonText = value;
                RaisePropertyChanged(nameof(SumButtonText));
            }
        }


        public bool PayEnabled => SelectedProducts.Count > 0;

        public ICommand EndShiftCommand { get; }
        public ICommand ShowCurrentSalesCommand { get; }
        public ICommand ShowExpenseCommand { get; }
        public ICommand EnablePoliceSaleCommand { get; }
        public ICommand EnableCreditCardSaleCommand { get; }
        public ICommand PayCommand {get;}
        public ICommand ItemSelectedCommand { get; }
        public ICommand ShowCurrentShiftExpensesCommand { get; set; }
        public ICommand ShowInventoryCommand { get; set; }
        public ICommand ShowUtilizeCommand { get; set; }
        public ICommand ShowSettingsCommand { get; set; }

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
        readonly ISyncManager syncManager;

        public MainViewModel(IMvxViewModelLoader mvxViewModelLoader, IProductManager productManager, ISyncManager syncManager)
        {
            this.syncManager = syncManager;
            this.productManager = productManager;

            var request = new MvxViewModelRequest(typeof(CoffeeViewModel));
            CoffeeProducts = (CoffeeViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(TeaViewModel));
            TeaProducts = (TeaViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(SweetsViewModel));
            SweetsProducts = (SweetsViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(WaterViewModel));
            WaterProducts = (WaterViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(AddsViewModel));
            AddsProducts = (AddsViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(MealsViewModel));
            MealsProducts = (MealsViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(ColdDrinksViewModel));
            ColdDrinksProducts = (ColdDrinksViewModel)mvxViewModelLoader.LoadViewModel(request, null);
            request = new MvxViewModelRequest(typeof(IceCreamViewModel));
            IceCreamProducts = (IceCreamViewModel)mvxViewModelLoader.LoadViewModel(request, null);

           
            EndShiftCommand = new MvxCommand(DoEndShift);
            ShowCurrentSalesCommand = new MvxCommand(() => ShowViewModel<CurrentShiftSalesViewModel>());
            ShowExpenseCommand = new MvxCommand(() => ShowViewModel<ExpenseViewModel>());
            EnablePoliceSaleCommand = new MvxCommand(() => IsPoliceSaleEnabled = !IsPoliceSaleEnabled);
            EnableCreditCardSaleCommand = new MvxCommand(() => IsCreditCardSaleEnabled = !IsCreditCardSaleEnabled);
            ItemSelectedCommand = new MvxCommand<SelectedProductViewModel>(DoSelectItem);
            PayCommand = new MvxAsyncCommand(DoPay);
            ShowCurrentShiftExpensesCommand = new MvxCommand(() => ShowViewModel<CurrentShiftExpensesViewModel>());
            ShowInventoryCommand= new MvxCommand(() => ShowViewModel<InventoryViewModel>());
            ShowUtilizeCommand = new MvxCommand(() => ShowViewModel<UtilizeProductsViewModel>());
            ShowSettingsCommand = new MvxCommand(() => ShowViewModel<SettingsViewModel>(new { isInitialSetup = false }));
        }

        public async void Init(int userId, int shiftId)
        {
            _userId = userId;
            _shiftId = shiftId;
            var tasks = new List<Task>();
            tasks.Add(CoffeeProducts.InitViewModel());
            tasks.Add(TeaProducts.InitViewModel());
            tasks.Add(SweetsProducts.InitViewModel());
            tasks.Add(WaterProducts.InitViewModel());
            tasks.Add(AddsProducts.InitViewModel());
            tasks.Add(MealsProducts.InitViewModel());
            tasks.Add(ColdDrinksProducts.InitViewModel());
            tasks.Add(IceCreamProducts.InitViewModel());
            await ExecuteSafe( async () => 
            {
                await Task.WhenAll(tasks);
            });
            allProducts = CoffeeProducts.Items
                .Concat(TeaProducts.Items)
                .Concat(SweetsProducts.Items)
                .Concat(WaterProducts.Items)
                .Concat(AddsProducts.Items)
                .Concat(MealsProducts.Items)
                .Concat(ColdDrinksProducts.Items)
                .Concat(IceCreamProducts.Items);

            SubscribeToSelectProduct();
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
        }


        private void DoSelectItem(SelectedProductViewModel obj)
        {
            SelectedProducts.Remove(obj);
            Sum -= (int)obj.Price;
        }

        private async Task DoPay()
        {
            if(SelectedProducts.Count <= 0)
            {
                return;
            }
            var tasks = SelectedProducts.Select(productViewModel =>
                                  productManager.SaleProduct(
                                _shiftId,
                                productViewModel.ProductId,
                                productViewModel.Price,
                                productViewModel.IsPoliceSale,
                                productViewModel.IsCreditCardSale,
                                productViewModel.IsSaleByWeight,
                                    productViewModel.Weight)
                                               );
            await ExecuteSafe(async () => await Task.WhenAll(tasks));
            SelectedProducts.Clear();
            Sum = 0;
        }

        protected override void DoUnsubscribe()
        {
            foreach (var item in allProducts)
            {
                item.ProductSelected -= OnProductSelected;
            }
        }

        private async void DoEndShift()
        {
            bool isAllSalesSynced = await ExecuteSafe( async () => await syncManager.SyncSales());
            if(!isAllSalesSynced)
            {
                Alert("Невозможно закрыть смену, продажи не синхронизированы");
                return;
            }
            Confirm("Завершить смену?", () => 
            {
                ShowViewModel<EndShiftViewModel>(new { shiftId = _shiftId });
                CloseCommand.Execute(null);
            });
        }

    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System.Linq;
using System.Text;
using CoffeManager.Common;
using System;
using CoffeeManager.Core.ViewModels.Inventory;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Core.ViewModels.Settings;
using CoffeeManager.Core.ViewModels.UtilizedProducts;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels.Motivation;

namespace CoffeeManager.Core.ViewModels
{
    public class MainViewModel : PageViewModel, IMvxViewModel<Shift>
    {
        private readonly IProductManager productManager;

        private Shift shiftInfo;

        private bool policeSaleEnabled;
        private bool isCreditCardSaleEnabled;
        private string sumButtonText;
        private int sum;
        private int? changeSum;
        private string userName;
        private ObservableCollection<SelectedProductViewModel> selectedProducts = new ObservableCollection<SelectedProductViewModel>();

        private MvxObservableCollection<CategoryItemViewModel> categoies = new MvxObservableCollection<CategoryItemViewModel>();
        private MvxObservableCollection<ProductViewModel> products = new MvxObservableCollection<ProductViewModel>();

        private readonly Action<int> onCategorySelectedAction;
        
        public bool IsPoliceSaleEnabled
        {
            get => policeSaleEnabled;
            set
            {
                policeSaleEnabled = value;
                RaisePropertyChanged(nameof(IsPoliceSaleEnabled));
                if(allProducts == null)
                {
                    return;
                }
                foreach (var item in allProducts)
                {
                    item.IsPoliceSale = policeSaleEnabled;
                }
            }
        }

        public bool IsCreditCardSaleEnabled
        {
            get => isCreditCardSaleEnabled;
            set
            {
                isCreditCardSaleEnabled = value;
                RaisePropertyChanged(nameof(IsCreditCardSaleEnabled));
                foreach (var item in allProducts)
                {
                    item.IsCreditCardSale = isCreditCardSaleEnabled;
                }
            }
        }

        public int Sum
        {
            get => sum;
            set
            {
                sum = value;
                RaisePropertyChanged(nameof(Sum));
                RaisePropertyChanged(nameof(PayEnabled));
                RaisePropertyChanged(nameof(SumButtonText));
                RaisePropertyChanged(nameof(ChangeSum));
            }
        }

        public int? ChangeSum
        {
            get 
            {
                if (changeSum.HasValue && Sum > 0)
                {
                    return Math.Abs(Sum - changeSum.Value);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                changeSum = value;
                RaisePropertyChanged(nameof(ChangeSum));
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
                sumButtonText = value;
                RaisePropertyChanged(nameof(SumButtonText));
            }
        }


        public bool PayEnabled => SelectedProducts.Count > 0;

        public string UserName 
        {
            get => userName;
            set
            {
                userName = value;
                RaisePropertyChanged();
            }
        }

        public int SelectedCategoryId
        {
            get => selectedCategoryId;
            set
            {
                selectedCategoryId = value;
                RaisePropertyChanged();
            }
        }

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
        public ICommand ShowMotivationCommand { get; set; }
        
        
        public ICommand ShowChargeCommand { get; set; }

        public ObservableCollection<SelectedProductViewModel> SelectedProducts
        {
            get => selectedProducts;
            set
            {
                selectedProducts = value;
                RaisePropertyChanged(nameof(SelectedProducts));
            }
        }
        
        public MvxObservableCollection<CategoryItemViewModel> Categories
        {
            get => categoies;
            set
            {
                categoies = value;
                RaisePropertyChanged(nameof(Categories));
            }
        }
        
        public MvxObservableCollection<ProductViewModel> Products
        {
            get => products;
            set
            {
                products = value;
                RaisePropertyChanged(nameof(Products));
            }
        }
        
        private IEnumerable<ProductItemViewModel> allProducts;
        readonly ISyncManager syncManager;
        readonly IUserManager userManager;
        private readonly ICategoryManager categoryManager;
        private int selectedCategoryId;

        public MainViewModel(IMvxViewModelLoader mvxViewModelLoader,
                             IProductManager productManager,
                             ISyncManager syncManager,
                             IUserManager userManager,
                            ICategoryManager categoryManager)
        {
            this.userManager = userManager;
            this.categoryManager = categoryManager;
            this.syncManager = syncManager;
            this.productManager = productManager;
            
            onCategorySelectedAction = i =>
            {
                foreach (var categoryItemViewModel in Categories)
                {
                    categoryItemViewModel.IsSelected = false;
                }
                SelectedCategoryId = i;
            };
           
            EndShiftCommand = new MvxCommand(DoEndShift);
            ShowCurrentSalesCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CurrentShiftSalesViewModel>());
            ShowExpenseCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ExpenseViewModel>());
            EnablePoliceSaleCommand = new MvxCommand(() => IsPoliceSaleEnabled = !IsPoliceSaleEnabled);
            EnableCreditCardSaleCommand = new MvxCommand(() => IsCreditCardSaleEnabled = !IsCreditCardSaleEnabled);
            ItemSelectedCommand = new MvxCommand<SelectedProductViewModel>(DoSelectItem);
            PayCommand = new MvxAsyncCommand(DoPay);
            ShowCurrentShiftExpensesCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CurrentShiftExpensesViewModel>());
            ShowInventoryCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<InventoryViewModel>());
            ShowUtilizeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UtilizeProductsViewModel>());
            ShowSettingsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<SettingsViewModel>());
            ShowMotivationCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MotivationViewModel>());
            ShowChargeCommand = new MvxCommand<int>((sum) => DoShowCharge(sum));
        }

        private void DoShowCharge(int sum)
        {
            ChangeSum = sum;
        }

        public override async Task Initialize()
        {
            var tasks = new List<Task>();

            var cats = await categoryManager.GetCategoriesForClient();
            var categories = cats.ToList();
            foreach (var category in categories)
            {
                var vm = new ProductViewModel(category);
                Products.Add(vm);
                tasks.Add(vm.InitViewModel());
            }

            Categories = new MvxObservableCollection<CategoryItemViewModel>(
                categories.Select(s => new CategoryItemViewModel(onCategorySelectedAction)
                {
                    Id = s.Id,
                    Name = s.Name
                }));
            Categories.First().IsSelected = true;
            
            await ExecuteSafe(async () =>
            {
                await Task.WhenAll(tasks);

                List<ProductItemViewModel> prods = new List<ProductItemViewModel>();
                foreach (var p in Products)
                {
                    if (p.HasSubCategories)
                    {
                        foreach (var subCategory in p.SubCategories)
                        {
                            prods.AddRange(subCategory.Items);
                        }
                    }
                    else
                    {
                        prods.AddRange(p.Items);
                    }
                }

                allProducts = prods;

                SubscribeToSelectProduct();

                var user = await userManager.GetUser(shiftInfo.UserId);
                UserName = user.Name;

            }, null, false);
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
            if(SelectedProducts.Count == 0)
            {
                ChangeSum = null;
            }
        }

        private async Task DoPay()
        {
            if(SelectedProducts.Count <= 0)
            {
                return;
            }
            var tasks = SelectedProducts.Select(productViewModel =>
                                  productManager.SaleProduct(
                                                    shiftInfo.Id,
                                productViewModel.ProductId,
                                productViewModel.Price,
                                productViewModel.IsPoliceSale,
                                productViewModel.IsCreditCardSale,
                                productViewModel.IsSaleByWeight,
                                    productViewModel.Weight)
                                               )
                                        .Concat(new[] { syncManager.SyncSales()});
            
            await ExecuteSafe(async () => await Task.WhenAll(tasks), null, false);
            SelectedProducts.Clear();
            Sum = 0;
            ChangeSum = null;
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
            Confirm("Завершить смену?",async () => 
            {
                await NavigationService.Navigate<EndShiftViewModel, int>(shiftInfo.Id);
            });
        }

        public void Prepare(Shift shiftInfo)
        {
            this.shiftInfo = shiftInfo;
        }
    }

}

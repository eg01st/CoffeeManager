using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeManager.Common.Managers;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Calculation;
using CoffeeManager.Common;
using CoffeeManager.Models.Data.Product;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;

namespace CoffeeManagerAdmin.Core.ViewModels.Products
{
    public class ProductDetailsViewModel : AdminCoffeeRoomFeedViewModel<ProductPaymentStrategyItemViewModel>, IMvxViewModel<int>
    {
        private readonly IProductManager manager;
        private readonly ICategoryManager categoryManager;
        
        private ProductDetaisDTO productDTO;
        private int id;
        private string name;
        private int cupType;
        private string cupTypeName;
        private int categoryId;
        private string categoryName;
        private string selectedColor;
        private string description;

        private Entity selectedCupType;
        private CategoryItemViewModel selectedCategory;
        private bool isSaleByWeight;

        private bool isPercentPaymentEnabled;
        
        #region Properties
        public List<Entity> CupTypesList => TypesLists.CupTypesList;
        public List<CategoryItemViewModel> CategoriesList { get; set; } = new List<CategoryItemViewModel>();
        public List<string> Colors { get; set; } = new List<string>();
        public ICommand SaveProductCommand { get; }
        private int productId;


        public Entity SelectedCupType
        {
            get { return selectedCupType; }
            set
            {
                if (selectedCupType != value)
                {
                    selectedCupType = value;
                    RaisePropertyChanged(nameof(SelectedCupType));
                    CupType = selectedCupType.Id;
                    CupTypeName = selectedCupType.Name;
                }
            }
        }

        public CategoryItemViewModel SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    RaisePropertyChanged(nameof(SelectedCategory));
                    CategoryId = selectedCategory.Id;
                    CategoryName = selectedCategory.Name;
                }
            }
        }

        public string SelectedColor
        {
            get => selectedColor;
            set => SetProperty(ref selectedColor, value);
        }

        public bool IsPercentPaymentEnabled
        {
            get => isPercentPaymentEnabled;
            set => SetProperty(ref isPercentPaymentEnabled, value);
        }

        public string PriceTitle {get;set;} = "Цена";   

        public bool IsSaleByWeight
        {
            get { return isSaleByWeight;}
            set
            {
                isSaleByWeight = value;
                PriceTitle = isSaleByWeight ? "Цена за 100 грамм" : "Цена";
                RaisePropertyChanged(nameof(IsSaleByWeight));
                RaisePropertyChanged(nameof(PriceTitle));
            }
        }
        
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        
        public int CupType
        {
            get { return cupType; }
            set
            {
                cupType = value;
                RaisePropertyChanged(nameof(CupType));
                RaisePropertyChanged(nameof(SelectedCupType));
            }
        }

        public string CupTypeName
        {
            get { return cupTypeName; }
            set
            {
                cupTypeName = value;
                RaisePropertyChanged(nameof(CupTypeName));
            }
        }

        public int CategoryId
        {
            get { return categoryId; }
            set
            {
                SetProperty(ref categoryId, value);
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        public string CategoryName
        {
            get => categoryName;
            set => SetProperty(ref categoryName, value);
        }

        public ICommand SelectCalculationItemsCommand { get; }
        
        public ICommand AddPaymentStrategyCommand { get; }
        
        public MvxObservableCollection<ProductPriceItemViewModel> ProductPrices { get; } = new MvxObservableCollection<ProductPriceItemViewModel>();
        
        #endregion


        public ProductDetailsViewModel(IProductManager manager, ICategoryManager categoryManager)
        {
            this.manager = manager;
            this.categoryManager = categoryManager;
         
            SelectCalculationItemsCommand = new MvxAsyncCommand(DoSelectCalculationItems);
            AddPaymentStrategyCommand = new MvxCommand(DoAddPaymentStrategy);
            
            SaveProductCommand = new MvxCommand(DoSaveProduct);
            
        }

        private void DoAddPaymentStrategy()
        {
            var availableCoffeeRooms = CoffeeRooms.Where(c => ItemsCollection.All(a => a.CoffeeRoomId != c.CoffeeRoomNo));
            var optionList = new List<ActionSheetOption>();
            foreach (var cr in availableCoffeeRooms)
            {
                optionList.Add(new ActionSheetOption(cr.Name, () =>  AddNewPaymentStrategy(cr)));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig
            {
                Options = optionList,
                Title = "Выбор заведения",
                Cancel = new ActionSheetOption("Отмена")
            });
        }

        private void AddNewPaymentStrategy(Entity coffeeRoom)
        {
            var vm = new ProductPaymentStrategyItemViewModel()
            {
                CoffeeRoomId = coffeeRoom.CoffeeRoomNo,
                CoffeeRoomName = coffeeRoom.Name,
                ProductId = productId
            };
            ItemsCollection.Add(vm);
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            await ExecuteSafe(async () =>
            {
                var categories = await categoryManager.GetCategoriesPlain();
                CategoriesList = categories.Select(s => new CategoryItemViewModel(s)).ToList();
                RaisePropertyChanged(nameof(CategoriesList));

                Colors = (await manager.GetAvaivalbeProductColors()).ToList();
                RaisePropertyChanged(nameof(Colors));

                productDTO = await manager.GetProduct(productId);

                id = productDTO.Id;
                Name = productDTO.Name;
                IsSaleByWeight = productDTO.IsSaleByWeight;
                SelectedColor = productDTO.Color;
                Description = productDTO.Description;
                IsPercentPaymentEnabled = productDTO.IsPercentPaymentEnabled;
                var cupType = CupTypesList.FirstOrDefault(t => t.Id == productDTO.CupType);
                if (cupType != null)
                {
                    SelectedCupType = cupType;
                }

                var productType = CategoriesList.FirstOrDefault(t => t.Id == productDTO.CategoryId);
                if (productType != null)
                {
                    SelectedCategory = productType;
                }

                if (productDTO.ProductPaymentStrategies != null)
                {
                    var vms = productDTO.ProductPaymentStrategies
                        .Select(s =>
                            new ProductPaymentStrategyItemViewModel(s,
                                CoffeeRooms.First(c => c.CoffeeRoomNo == s.CoffeeRoomId).Name));
                    ItemsCollection.ReplaceWith(vms);
                }
                
                if (productDTO.ProductPrices != null)
                {
                    var vms = productDTO.ProductPrices
                        .Select(s =>
                            new ProductPriceItemViewModel(s,
                                CoffeeRooms.First(c => c.CoffeeRoomNo == s.CoffeeRoomNo).Name));
                    ProductPrices.ReplaceWith(vms);
                }
            });
        }

        private void DoSaveProduct()
        {
            UserDialogs.Confirm(new ConfirmConfig()
            {
                Message = $"Сохранить изменения в продукте \"{Name}\"?",
                OnAction = async (obj) =>
                {
                    if (obj)
                    {
                        await ExecuteSafe(async () =>
                        {
                            var dto = new ProductDetaisDTO()
                            {
                                Id = id,
                                Name = name,
                                CupType = cupType,
                                CoffeeRoomNo = Config.CoffeeRoomNo,
                                IsSaleByWeight = isSaleByWeight,
                                CategoryId = CategoryId,
                                Color = SelectedColor,
                                Description = Description,
                                IsPercentPaymentEnabled = IsPercentPaymentEnabled
                            };
                            
                            if (IsPercentPaymentEnabled)
                            {
                                var strategies = ItemsCollection.Select(s => new ProductPaymentStrategyDTO()
                                {
                                    CoffeeRoomId = s.CoffeeRoomId,
                                    Id = s.Id,
                                    ProductId = s.ProductId,
                                    DayShiftPercent = s.DayShiftPersent,
                                    NightShiftPercent = s.NightShiftPercent
                                }).ToList();
                                dto.ProductPaymentStrategies = strategies;
                            }

                            var prices = ProductPrices.Select(s => new ProductPriceDTO()
                            {
                                CoffeeRoomNo = s.CoffeeRoomId,
                                Id = s.Id,
                                ProductId = s.ProductId,
                                Price = s.Price,
                                DiscountPrice = s.DiscountPrice
                            }).ToList();
                            
                            dto.ProductPrices = prices;
                            
                            await manager.EditProduct(dto);
                            MvxMessenger.Publish(new ProductListChangedMessage(this));
                            await NavigationService.Close(this);
                        });
                    }
                }
            });
        }

        private async Task DoSelectCalculationItems()
        {
           await NavigationService.Navigate<CalculationViewModel, int>(id);
        }

        public void Prepare(int parameter)
        {
            productId = parameter;
        }
    }
}

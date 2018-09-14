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
        private string price;
        private string policePrice;
        private int cupType;
        private string cupTypeName;
        private int categoryId;
        private string productTypeName;
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
        public ICommand AddProductCommand => addProductCommand;
        private ICommand addProductCommand;
        private int productId;

        public bool IsAddEnabled => !string.IsNullOrEmpty(Name) 
                                    && !string.IsNullOrEmpty(Price) 
                                    && !string.IsNullOrEmpty(PolicePrice) 
                                    && !string.IsNullOrEmpty(CupTypeName)
                                    && !string.IsNullOrEmpty(CategoryName)
                                    && !string.IsNullOrEmpty(SelectedColor);

        public Entity SelectedCupType
        {
            get { return selectedCupType; }
            set
            {
                if (selectedCupType != value)
                {
                    selectedCupType = value;
                    RaisePropertyChanged(nameof(SelectedCupType));
                    RaisePropertyChanged(nameof(IsAddEnabled));
                    CupType = selectedCupType.Id;
                    CupTypeName = selectedCupType.Name;
                }
            }
        }

        public CategoryItemViewModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    RaisePropertyChanged(nameof(SelectedCategory));
                    RaisePropertyChanged(nameof(IsAddEnabled));
                    CategoryId = selectedCategory.Id;
                    CategoryName = selectedCategory.Name;
                }
            }
        }

        public string SelectedColor
        {
            get => selectedColor;
            set
            {
                SetProperty(ref selectedColor, value); 
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
        }

        public bool IsPercentPaymentEnabled
        {
            get => isPercentPaymentEnabled;
            set => SetProperty(ref isPercentPaymentEnabled, value);
        }

        public string ButtonTitle {get;set;} = "Добавить продукт";
        public string PriceTitle {get;set;} = "Цена: ";
        public string PolicePriceTitle {get;set;} = "Цена по скидке: ";        

        public bool IsSaleByWeight
        {
            get { return isSaleByWeight;}
            set
            {
                isSaleByWeight = value;
                PriceTitle = isSaleByWeight ? "Цена за 100 грамм: " : "Цена: ";
                PolicePriceTitle = isSaleByWeight ? "Цена по скидке за 100 грамм: " : "Цена по скидке:";
                RaisePropertyChanged(nameof(IsSaleByWeight));
                RaisePropertyChanged(nameof(PriceTitle));
                RaisePropertyChanged(nameof(PolicePriceTitle));
                
            }
        }
        
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged(nameof(Price));
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
        }

        public string PolicePrice
        {
            get { return policePrice; }
            set
            {
                policePrice = value;
                RaisePropertyChanged(nameof(PolicePrice));
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
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
                categoryId = value;
                RaisePropertyChanged(nameof(CategoryId));
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        public string CategoryName
        {
            get { return productTypeName; }
            set
            {
                productTypeName = value;
                RaisePropertyChanged(nameof(CategoryName));
            }
        }

        public ICommand SelectCalculationItemsCommand { get; }
        
        public ICommand AddPaymentStrategyCommand { get; }
        
        #endregion


        public ProductDetailsViewModel(IProductManager manager, ICategoryManager categoryManager)
        {
            this.manager = manager;
            this.categoryManager = categoryManager;
         
            SelectCalculationItemsCommand = new MvxAsyncCommand(DoSelectCalculationItems);
            AddPaymentStrategyCommand = new MvxCommand(DoAddPaymentStrategy);
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
            
            var categories = await categoryManager.GetCategoriesPlain();
            CategoriesList = categories.Select(s => new CategoryItemViewModel(s)).ToList();
            RaisePropertyChanged(nameof(CategoriesList));

            Colors = (await manager.GetAvaivalbeProductColors()).ToList();
            RaisePropertyChanged(nameof(Colors));
            
            if(productId > 0)
            {
                productDTO = await manager.GetProduct(productId);

                addProductCommand = new MvxCommand(DoEditProduct);
                id = productDTO.Id;
                Name = productDTO.Name;
                Price = productDTO.Price.ToString("F");
                PolicePrice = productDTO.PolicePrice.ToString("F");
                IsSaleByWeight = productDTO.IsSaleByWeight;
                SelectedColor = productDTO.Color;
                Description = productDTO.Description;
                IsPercentPaymentEnabled = productDTO.IsPercentPaymentEnabled;
                var cupType = CupTypesList.FirstOrDefault(t => t.Id == productDTO.CupType);
                if(cupType != null)
                {
                    SelectedCupType = cupType;
                }
                var productType = CategoriesList.FirstOrDefault(t => t.Id == productDTO.CategoryId);
                if(productType != null)
                {
                    SelectedCategory = productType;
                }
                if(productDTO.ProductPaymentStrategies != null)
                {
                    var vms = productDTO.ProductPaymentStrategies
                        .Select(s => new ProductPaymentStrategyItemViewModel(s, CoffeeRooms.First(c => c.CoffeeRoomNo == s.CoffeeRoomId).Name));
                    ItemsCollection.ReplaceWith(vms);
                }
                ButtonTitle = "Сохранить изменения";
                RaisePropertyChanged(nameof(ButtonTitle));
            }
            else
            {
                addProductCommand = new MvxCommand(DoAddProduct);
            }
            RaisePropertyChanged(nameof(AddProductCommand));
        }

        private void DoAddProduct()
        {
            UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig()
            {
                Message = $"Добавить продукт \"{Name}\"?",
                OnAction = async (obj) =>
                {
                    if (obj)
                    {
                        await ExecuteSafe(async () =>
                        {
                            var dto = new ProductDetaisDTO()
                            {
                                Name = name,
                                Price = decimal.Parse(price),
                                PolicePrice = decimal.Parse(policePrice),
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

                            await manager.AddProduct(dto);
                            MvxMessenger.Publish(new ProductListChangedMessage(this));
                            await NavigationService.Close(this);
                        });
                    }
                }
            });
        }

        private void DoEditProduct()
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
                                Price = decimal.Parse(price),
                                PolicePrice = decimal.Parse(policePrice),
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

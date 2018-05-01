using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.Categories;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using CoffeeManagerAdmin.Core.ViewModels.Calculation;
using CoffeeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels.Products
{
    public class ProductDetailsViewModel : ViewModelBase, IMvxViewModel<Product>
    {
        private Product product;
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

        #region Properties
        public List<Entity> CupTypesList => TypesLists.CupTypesList;
        public List<CategoryItemViewModel> CategoriesList { get; set; } = new List<CategoryItemViewModel>();
        public List<string> Colors { get; set; } = new List<string>();
        public ICommand AddProductCommand => addProductCommand;
        private ICommand addProductCommand;
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

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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

        readonly IProductManager manager;
        private readonly ICategoryManager categoryManager;


        public ICommand SelectCalculationItemsCommand { get; }

        #endregion


        public ProductDetailsViewModel(IProductManager manager, ICategoryManager categoryManager)
        {
            this.manager = manager;
            this.categoryManager = categoryManager;
         
            SelectCalculationItemsCommand = new MvxAsyncCommand(DoSelectCalculationItems);
        }

        public override async Task Initialize()
        {
            var categories = await categoryManager.GetCategoriesPlain();
            CategoriesList = categories.Select(s => new CategoryItemViewModel(s)).ToList();
            RaisePropertyChanged(nameof(CategoriesList));

            Colors = (await manager.GetAvaivalbeProductColors()).ToList();
            RaisePropertyChanged(nameof(Colors));
            
            if(product != null)
            {
                addProductCommand = new MvxCommand(DoEditProduct);
                id = product.Id;
                Name = product.Name;
                Price = product.Price.ToString("F");
                PolicePrice = product.PolicePrice.ToString("F");
                IsSaleByWeight = product.IsSaleByWeight;
                SelectedColor = product.Color;
                
                var cupType = CupTypesList.FirstOrDefault(t => t.Id == product.CupType);
                if(cupType != null)
                {
                    SelectedCupType = cupType;
                }
                var productType = CategoriesList.FirstOrDefault(t => t.Id == product.CategoryId);
                if(productType != null)
                {
                    SelectedCategory = productType;
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
                            await manager.AddProduct(new Product() 
                             { 
                                Name = name,
                                Price = decimal.Parse(price),
                                PolicePrice = decimal.Parse(policePrice),
                                CupType = cupType,
                                CoffeeRoomNo = Config.CoffeeRoomNo,
                                IsSaleByWeight = isSaleByWeight,
                                CategoryId = CategoryId,
                                Color = SelectedColor,
                                Description = Description
                            });
                            Publish(new ProductListChangedMessage(this));
                            Close(this);
                        });
                    }
                }
            });
        }

        private void DoEditProduct()
        {
            UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig()
            {
                Message = $"Сохранить изменения в продукте \"{Name}\"?",
                OnAction = async (obj) =>
                {
                    if (obj)
                    {
                        await ExecuteSafe(async () =>
                        {
                            await manager.EditProduct(new Product()
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
                                Description = Description
                            });
                            Publish(new ProductListChangedMessage(this));
                            Close(this);
                        });
                    }
                }
            });
        }

        private async Task DoSelectCalculationItems()
        {
           await NavigationService.Navigate<CalculationViewModel, int>(id);
        }

        public void Prepare(Product parameter)
        {
            product = parameter;
        }
    }
}

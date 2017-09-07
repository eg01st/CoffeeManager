using System.Collections.Generic;
using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Linq;
using CoffeeManagerAdmin.Core.Util;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
   
        private int _id;
        private string _name;
        private string _price;
        private string _policePrice;
        private int _cupType;
        private string _cupTypeName;
        private int _productType;
        private string _productTypeName;
        private ICommand _addProductCommand;
        private Entity _selectedCupType;
        private Entity _selectedProductType;
        private bool isSaleByWeight;

        #region Properties
        public List<Entity> CupTypesList => TypesLists.CupTypesList;
        public List<Entity> ProductTypesList => TypesLists.ProductTypesList;
        public ICommand AddProductCommand => _addProductCommand;
        public bool IsAddEnabled => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Price) && !string.IsNullOrEmpty(PolicePrice) && !string.IsNullOrEmpty(CupTypeName) && !string.IsNullOrEmpty(ProductTypeName);

        public Entity SelectedCupType
        {
            get { return _selectedCupType; }
            set
            {
                if (_selectedCupType != value)
                {
                    _selectedCupType = value;
                    RaisePropertyChanged(nameof(SelectedCupType));
                    RaisePropertyChanged(nameof(IsAddEnabled));
                    CupType = _selectedCupType.Id;
                    CupTypeName = _selectedCupType.Name;
                }
            }
        }

        public Entity SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                if (_selectedProductType != value)
                {
                    _selectedProductType = value;
                    RaisePropertyChanged(nameof(SelectedProductType));
                    RaisePropertyChanged(nameof(IsAddEnabled));
                    ProductTypeId = _selectedProductType.Id;
                    ProductTypeName = _selectedProductType.Name;
                }
            }
        }

        public string ButtonTitle {get;set;} = "Добавить продукт";
        public string PriceTitle {get;set;} = "Цена: ";
        public string PolicePriceTitle {get;set;} = "Цена для копов: ";        

        public bool IsSaleByWeight
        {
            get { return isSaleByWeight;}
            set
            {
                isSaleByWeight = value;
                PriceTitle = isSaleByWeight ? "Цена за 100 грамм: " : "Цена: ";
                PolicePriceTitle = isSaleByWeight ? "Цена для копов за 100 грамм: " : "Цена для копов:";
                RaisePropertyChanged(nameof(IsSaleByWeight));
                RaisePropertyChanged(nameof(PriceTitle));
                RaisePropertyChanged(nameof(PolicePriceTitle));
                
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
        }

        public string PolicePrice
        {
            get { return _policePrice; }
            set
            {
                _policePrice = value;
                RaisePropertyChanged(nameof(PolicePrice));
                RaisePropertyChanged(nameof(IsAddEnabled));
            }
        }

        public int CupType
        {
            get { return _cupType; }
            set
            {
                _cupType = value;
                RaisePropertyChanged(nameof(CupType));
                RaisePropertyChanged(nameof(SelectedCupType));
            }
        }

        public string CupTypeName
        {
            get { return _cupTypeName; }
            set
            {
                _cupTypeName = value;
                RaisePropertyChanged(nameof(CupTypeName));
            }
        }

        public int ProductTypeId
        {
            get { return _productType; }
            set
            {
                _productType = value;
                RaisePropertyChanged(nameof(ProductTypeId));
                RaisePropertyChanged(nameof(SelectedProductType));
            }
        }

        public string ProductTypeName
        {
            get { return _productTypeName; }
            set
            {
                _productTypeName = value;
                RaisePropertyChanged(nameof(ProductTypeName));
            }
        }

        readonly IProductManager manager;

        public ICommand SelectCalculationItemsCommand { get; }

        #endregion


        public ProductDetailsViewModel(IProductManager manager)
        {
            this.manager = manager;
            _addProductCommand = new MvxCommand(DoAddProduct);
            SelectCalculationItemsCommand = new MvxCommand(DoSelectCalculationItems);
        }

        public void Init(Guid id)
        {
            if(id != Guid.Empty)
            {
                _addProductCommand = new MvxCommand(DoEditProduct);
                
                Product product;
                ParameterTransmitter.TryGetParameter(id, out product);
                _id = product.Id;
                Name = product.Name;
                Price = product.Price.ToString("F");
                PolicePrice = product.PolicePrice.ToString("F");
                IsSaleByWeight = product.IsSaleByWeight;
                
                var cupType = CupTypesList.FirstOrDefault(t => t.Id == product.CupType);
                if(cupType != null)
                {
                    SelectedCupType = cupType;
                }
                var productType = ProductTypesList.FirstOrDefault(t => t.Id == product.ProductType);
                if(productType != null)
                {
                    SelectedProductType = productType;
                }
                ButtonTitle = "Сохранить изменения";
                RaisePropertyChanged(nameof(ButtonTitle));
            }
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
                        await manager.AddProduct(Name, Price, PolicePrice, CupType, ProductTypeId, IsSaleByWeight);
                        Publish(new ProductListChangedMessage(this));
                        Close(this);
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
                        await manager.EditProduct(_id, Name, Price, PolicePrice, CupType, ProductTypeId, IsSaleByWeight);
                        Publish(new ProductListChangedMessage(this));
                        Close(this);
                    }
                }
            });
        }

        private void DoSelectCalculationItems()
        {
            ShowViewModel<CalculationViewModel>(new { id = _id });
        }
     
    }
}

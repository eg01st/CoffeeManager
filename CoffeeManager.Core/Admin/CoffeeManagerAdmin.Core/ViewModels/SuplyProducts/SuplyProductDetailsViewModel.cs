using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class SuplyProductDetailsViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private decimal _supliedPrice;
        private decimal? _salePrice;
        private decimal? _itemCount;
        private decimal _expenseNumerationMultiplier;
        private string _expenseNumerationName;
        private decimal _inventoryNumerationMultiplier;
        private string _inventoryNumerationName;
        private bool inventoryEnabled;
        private ICommand _saveCommand;

        private ICommand _deleteCommand;

        public ICommand SaveCommand => _saveCommand;
        public ICommand DeleteCommand => _deleteCommand;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string ExpenseNumerationName
        {
            get { return _expenseNumerationName; }
            set
            {
                _expenseNumerationName = value;
                RaisePropertyChanged(nameof(ExpenseNumerationName));
            }
        }

        public string InventoryNumerationName
        {
            get { return _inventoryNumerationName; }
            set
            {
                _inventoryNumerationName = value;
                RaisePropertyChanged(nameof(InventoryNumerationName));
            }
        }

        public decimal SupliedPrice
        {
            get { return _supliedPrice; }
            set
            {
                _supliedPrice = value;
                RaisePropertyChanged(nameof(SupliedPrice));
            }
        }

        public decimal? SalePrice
        {
            get { return _salePrice; }
            set
            {
                _salePrice = value;
                RaisePropertyChanged(nameof(SalePrice));
            }
        }


        public decimal? ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = value;
                RaisePropertyChanged(nameof(ItemCount));
            }
        }

        public decimal ExpenseNumerationMultiplier
        {
            get { return _expenseNumerationMultiplier; }
            set
            {
                _expenseNumerationMultiplier = value;
                RaisePropertyChanged(nameof(ExpenseNumerationMultiplier));
            }
        }

        public decimal InventoryNumerationMultiplier
        {
            get { return _inventoryNumerationMultiplier; }
            set
            {
                _inventoryNumerationMultiplier = value;
                RaisePropertyChanged(nameof(InventoryNumerationMultiplier));
            }
        }

        public bool InventoryEnabled
        {
            get { return inventoryEnabled; }
            set
            {
                inventoryEnabled = value;
                RaisePropertyChanged(nameof(InventoryEnabled));
                ExecuteSafe(() => inventoryManager.ToggleItemInventoryEnabled(_id));

            }
        }

        readonly ISuplyProductsManager manager;
        readonly IInventoryManager inventoryManager;

        public SuplyProductDetailsViewModel(ISuplyProductsManager manager, IInventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
            this.manager = manager;
            _saveCommand = new MvxCommand(DoSaveProduct);
            _deleteCommand = new MvxCommand(DoDeleteProduct);
        }

        private void DoDeleteProduct()
        {
            UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig()
            {
                Message = $"Действительно удалить баланс продукта \"{Name}\"?",
                OnAction = async (obj) =>
                {
                    if (obj)
                    {
                        await manager.DeleteSuplyProduct(_id);
                        Publish(new SuplyListChangedMessage(this));
                        Close(this);
                    }
                }
            });

        }

        private async void DoSaveProduct()
        {
            await ExecuteSafe(async () =>
            {
			    await manager.EditSuplyProduct(new CoffeeManager.Models.SupliedProduct()
			    {
			        Id = _id,
			        CoffeeRoomNo = Config.CoffeeRoomNo,
			        ExpenseNumerationMultyplier = ExpenseNumerationMultiplier,
			        ExpenseNumerationName = ExpenseNumerationName,
			        InventoryNumerationName = InventoryNumerationName,
			        InventoryNumerationMultyplier = InventoryNumerationMultiplier,
			        Price = SupliedPrice,
			        Quatity = ItemCount,
			    });
            });
            Publish(new SuplyListChangedMessage(this));
            Close(this);
        }

        public async void Init(int id)
        {
            _id = id;
            await ExecuteSafe(async () =>
           {
               var product = await manager.GetSuplyProduct(id);
               Name = product.Name;
               SupliedPrice = product.Price;
               SalePrice = product.SalePrice;
               ItemCount = product.Quatity;
               ExpenseNumerationName = product.ExpenseNumerationName;
               ExpenseNumerationMultiplier = product.ExpenseNumerationMultyplier;
               InventoryNumerationName = product.InventoryNumerationName;
               InventoryNumerationMultiplier = product.InventoryNumerationMultyplier;
               inventoryEnabled = product.InventoryEnabled;
               RaisePropertyChanged(nameof(InventoryEnabled));
           });
        }
    }
}

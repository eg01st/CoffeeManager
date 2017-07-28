using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
namespace CoffeeManagerAdmin.Core
{
    public class SuplyProductDetailsViewModel : ViewModelBase
    {
        private SuplyProductsManager manager = new SuplyProductsManager();

        private int _id;
        private string _name;
        private decimal _supliedPrice;
        private decimal? _salePrice;
        private decimal? _itemCount;
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

        public SuplyProductDetailsViewModel()
        {
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
            await manager.SaveSuplyProduct(_id, Name, SupliedPrice, ItemCount);
            Publish(new SuplyListChangedMessage(this));
            Close(this);
        }

        public async void Init(int id)
        {
            _id = id;
            var product = await manager.GetSupliedProduct(id);
            Name = product.Name;
            SupliedPrice = product.Price;
            SalePrice = product.SalePrice;
            ItemCount = product.Quatity;
        }
    }
}

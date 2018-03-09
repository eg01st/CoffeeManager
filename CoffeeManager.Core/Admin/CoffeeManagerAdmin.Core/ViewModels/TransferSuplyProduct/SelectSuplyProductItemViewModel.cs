using System;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Platform;
using System.Windows.Input;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
namespace CoffeeManagerAdmin.Core
{
    public class SelectSuplyProductItemViewModel : ListItemViewModelBase
    {
        private SupliedProduct item;
        private decimal maxQuantity;
        private decimal? quantityToTransfer;
        private bool isSelected;

        public int SuplyProductId => item.Id;   
        public string ExpenseTypeName => item.ExpenseTypeName;

        private bool CanDiscard()
        {
            return isSelected;
        }

        private void DoDiscard()
        {
            Confirm($"Отменить перевод продукта {Name}?", () => 
            {
                IsSelected = false;
                QuantityToTransfer = null;
            });

        }

        public ICommand DiscardTransferCommand { get; }

        public decimal? QuantityToTransfer
        {
            get { return quantityToTransfer; }
            set
            {
                quantityToTransfer = value;
                RaisePropertyChanged(nameof(QuantityToTransfer));
                RaisePropertyChanged(nameof(DiscardTransferCommand));
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        public SelectSuplyProductItemViewModel(SupliedProduct s)
        {
            this.item = s;
            Name = s.Name;
            maxQuantity = s.Quatity.Value;
            DiscardTransferCommand = new MvxCommand(DoDiscard, CanDiscard);
        }

        protected async override void DoGoToDetails()
        {
            var quantity = await PromtDecimalAsync($"Введите количество товара. Максимум {maxQuantity}");
            if(quantity.HasValue)
            {
                if(quantity.Value > maxQuantity)
                {
                    Alert("Вы пытаетесь перевести продуктов больше чем есть на балансе склада!");
                    return;
                }
                QuantityToTransfer = quantity;
                IsSelected = true;
            }
        }
    }
}

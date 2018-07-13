using CoffeeManager.Models.Data.Product;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Products
{
    public class ProductPaymentStrategyItemViewModel : FeedItemElementViewModel
    {
        private decimal dayShiftPersent;
        private decimal nightShiftPercent;
        
        public int Id { get; set; }
        public int CoffeeRoomId { get; set; }
        public int ProductId { get; set; }
        public string CoffeeRoomName { get; set; }

        public decimal DayShiftPersent
        {
            get => dayShiftPersent;
            set => SetProperty(ref dayShiftPersent, value);
        }
        
        public decimal NightShiftPercent
        {
            get => nightShiftPercent;
            set => SetProperty(ref nightShiftPercent, value);
        }

        public ProductPaymentStrategyItemViewModel()
        {
            
        }
        
        public ProductPaymentStrategyItemViewModel(ProductPaymentStrategyDTO dto, string coffeeRoomName)
        {
            Id = dto.Id;
            CoffeeRoomId = dto.CoffeeRoomId;
            DayShiftPersent = dto.DayShiftPercent;
            NightShiftPercent = dto.NightShiftPercent;
            ProductId = dto.ProductId;
            CoffeeRoomName = coffeeRoomName;
        }
    }
}
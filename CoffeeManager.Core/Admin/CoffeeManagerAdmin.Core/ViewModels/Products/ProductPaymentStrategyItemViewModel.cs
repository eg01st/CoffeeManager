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
        
        public ProductPaymentStrategyItemViewModel(ProductPaymentStrategyDTO dto)
        {
            Id = dto.Id;
            CoffeeRoomId = dto.CoffeeRoomId;
            DayShiftPersent = dto.DayShiftPersent;
            NightShiftPercent = dto.NightShiftPercent;
        }
    }
}
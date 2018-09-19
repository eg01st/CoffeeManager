using CoffeeManager.Models.Data.Product;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Products
{
    public class ProductPriceItemViewModel : FeedItemElementViewModel
    {
        private decimal price;
        private decimal discountPrice;
        
        public int Id { get; set; }
        public int CoffeeRoomId { get; set; }
        public int ProductId { get; set; }
        public string CoffeeRoomName { get; set; }

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        
        public decimal DiscountPrice
        {
            get => discountPrice;
            set => SetProperty(ref discountPrice, value);
        }

        public ProductPriceItemViewModel()
        {
            
        }
        
        public ProductPriceItemViewModel(ProductPriceDTO dto, string coffeeRoomName)
        {
            Id = dto.Id;
            CoffeeRoomId = dto.CoffeeRoomNo;
            Price = dto.Price;
            DiscountPrice = dto.DiscountPrice;
            ProductId = dto.ProductId;
            CoffeeRoomName = coffeeRoomName;
        }
    }
}
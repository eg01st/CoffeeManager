using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core
{
    public class SelectedProductViewModel : ViewModelBase
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public bool IsPoliceSale { get; set; }

        public bool IsSaleByWeight { get; set; }
        public decimal? Weight { get; set; }
        public bool IsCreditCardSale { get; set; }
        public string Name { get; set; }

        public SelectedProductViewModel(int productId, string name, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight)
        {
            Name = name;
            IsCreditCardSale = isCreditCardSale;
            Weight = weight;
            IsSaleByWeight = isSaleByWeight;
            IsPoliceSale = isPoliceSale;
            Price = price;
            ProductId = productId;
        }

    }
}

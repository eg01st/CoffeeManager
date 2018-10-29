using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder.History
{
    public class SuplyProductOrderItemViewModel : FeedItemElementViewModel
    {
        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public string SuplyProductName { get; set; }
        public int QuantityBefore { get; set; }
        public int OrderedQuantity { get; set; }
    }
}
using System;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder.History
{
    public class OrderHistoryItemViewModel : FeedItemElementViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
using System;

using UIKit;
using MobileCore.iOS.ViewControllers;
using CoffeeManagerAdmin.Core.ViewModels.AutoOrder;

namespace CoffeeManagerAdmin.iOS
{
    public partial class AutoOrderDetailsView : ViewControllerBase<AutoOrderDetailsViewModel>
    {
        public AutoOrderDetailsView() : base("AutoOrderDetailsView", null)
        {
        }
    }
}


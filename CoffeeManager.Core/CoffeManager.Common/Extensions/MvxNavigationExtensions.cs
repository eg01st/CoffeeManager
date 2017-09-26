using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace CoffeManager.Common
{
    public static class MvxNavigationExtensions
    {
        public const string RootNavigationKey = "root";
        public const string RootNavigationValue = "true";

        public static bool IsRootRequest(this MvxViewModelRequest request)
        {
            var presentationValues = request.PresentationValues;
            return presentationValues != null
                    && presentationValues.ContainsKey(RootNavigationKey)
                    && presentationValues[RootNavigationKey].Equals(RootNavigationValue);
        }

        public static IMvxBundle ProduceRootViewModelRequest()
        {
            return new MvxBundle(new Dictionary<string, string> { [RootNavigationKey] = RootNavigationValue });
        }
    }
}

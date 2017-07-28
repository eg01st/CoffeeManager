using System;
using System.Globalization;
using Android.Views;
using CoffeeManager.Models;
using MvvmCross.Platform.Converters;

namespace CoffeeManager.Droid.Converters
{
    public class CupTypeToVisibility : MvxValueConverter<CupTypeEnum, ViewStates>
    {
        protected override ViewStates Convert(CupTypeEnum value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == CupTypeEnum.Unknown ? ViewStates.Gone : ViewStates.Visible;
        }
    }
}
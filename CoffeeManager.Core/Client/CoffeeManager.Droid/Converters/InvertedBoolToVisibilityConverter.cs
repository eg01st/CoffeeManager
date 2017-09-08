using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Platform.Converters;

namespace CoffeeManager.Droid
{
    public class InvertedBoolToVisibilityConverter : MvxValueConverter<bool, ViewStates>
    {
        protected override ViewStates Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? ViewStates.Gone : ViewStates.Visible;
        }
    }
}

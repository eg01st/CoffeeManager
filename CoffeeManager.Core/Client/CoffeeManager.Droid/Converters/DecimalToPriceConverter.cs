using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace CoffeeManager.Droid.Converters
{
    public class DecimalToPriceConverter : MvxValueConverter<decimal?, String>
    {
        protected override string Convert(decimal? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString("####");
        }

        protected override decimal? ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrEmpty(value))
            {
                return null;
            }
            return decimal.Parse(value);
        }
    }
}
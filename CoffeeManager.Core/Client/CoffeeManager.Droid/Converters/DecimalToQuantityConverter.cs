
using System;
using System.Collections.Generic;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace CoffeeManager.Droid.Converters
{
    public class DecimalToQuantityConverter : MvxValueConverter<decimal?, String>
    {
        protected override string Convert(decimal? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString("##.##", System.Globalization.CultureInfo.InvariantCulture);
        }

        protected override decimal? ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return decimal.Parse(value);
        }
    }
}

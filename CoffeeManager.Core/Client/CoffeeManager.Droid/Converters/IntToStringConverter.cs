using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace CoffeeManager.Droid.Converters
{
    public class IntToStringConverter : MvxValueConverter<int?, string>
    {
        protected override string Convert(int? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString("##.##", System.Globalization.CultureInfo.InvariantCulture);
        }

        protected override int? ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return int.Parse(value);
        }
    }
}
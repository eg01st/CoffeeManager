using System;
using System.Globalization;
using CoffeeManager.Models;
using MvvmCross.Platform.Converters;

namespace CoffeeManager.Droid.Converters
{
    public class CupTypeToNameConverter : MvxValueConverter<int, string>
    {
        protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((CupTypeEnum) value).ToString().Substring(1);
        }
    }
}
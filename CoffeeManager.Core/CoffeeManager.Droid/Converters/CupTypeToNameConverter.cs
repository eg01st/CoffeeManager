using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
using System;
using System.Globalization;
using Android.Graphics;
using Android.Graphics.Drawables;
using CoffeeManager.Models;
using MvvmCross.Platform.Converters;
using Android.Content.Res;


namespace CoffeeManager.Droid.Converters
{
    public class CupTypeToColorConverter : MvxValueConverter<CupTypeEnum, ColorDrawable>
    {
        private static readonly Color Orange = Android.App.Application.Context.Resources.GetColor(Resource.Color.Orange);
        private static readonly Color Violet = Android.App.Application.Context.Resources.GetColor(Resource.Color.Violet);
        private static readonly Color Green = Android.App.Application.Context.Resources.GetColor(Resource.Color.LightGreen);
        private static readonly Color Transparent = Android.App.Application.Context.Resources.GetColor(Resource.Color.Transparent);

        protected override ColorDrawable Convert(CupTypeEnum value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                    case CupTypeEnum.c250:
                    return new ColorDrawable(Orange);
                    case CupTypeEnum.c400:
                    return new ColorDrawable(Violet);
                    case CupTypeEnum.c500:
                    return new ColorDrawable(Green);
                default:
                    return new ColorDrawable(Transparent);
            }
        }
    }
}
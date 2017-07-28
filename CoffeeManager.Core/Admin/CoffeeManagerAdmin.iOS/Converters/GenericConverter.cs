using System;
using MvvmCross.Platform.Converters;

namespace CoffeeManagerAdmin.iOS
{

    public class GenericConverter<TFrom, TTo> : MvxValueConverter<TFrom, TTo>
    {
        protected Func<TFrom, TTo> convertFunction;
        protected Func<TTo, TFrom> convertBackFunction;

        public GenericConverter(Func<TFrom, TTo> convertFunction, Func<TTo, TFrom> convertBackFunction = null)
        {
            this.convertFunction = convertFunction;
            this.convertBackFunction = convertBackFunction;
        }

        protected override TTo Convert(TFrom value, System.Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return convertFunction == null ? default(TTo) : convertFunction.Invoke(value);
        }

        protected override TFrom ConvertBack(TTo value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (convertBackFunction != null)
            {
                var convertValue = convertBackFunction(value);
                return convertValue;
            }

            return base.ConvertBack(value, targetType, parameter, culture);
        }
    }
}



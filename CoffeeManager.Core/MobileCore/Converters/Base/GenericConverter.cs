using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.IoC;

namespace MobileCore.Droid
{
    [MvxUnconventional]
    public class GenericConverter<TFrom, TTo> : MvxValueConverter<TFrom, TTo>
    {
        private Func<TFrom, TTo> convertFunction;
        private Func<TTo, TFrom> convertBackFunction;

        public GenericConverter(Func<TFrom, TTo> convertFunction, Func<TTo, TFrom> convertBackFunction = null)
        {
            this.convertFunction = convertFunction;
            this.convertBackFunction = convertBackFunction;
        }

        protected override TTo Convert(TFrom value, Type targetType, object parameter, CultureInfo culture)
            => convertFunction == null ? default(TTo) : convertFunction(value);

        protected override TFrom ConvertBack(TTo value, Type targetType, object parameter, CultureInfo culture)
        {
            return convertBackFunction == null
                ? base.ConvertBack(value, targetType, parameter, culture)
                : convertBackFunction(value);
        }
    }
}

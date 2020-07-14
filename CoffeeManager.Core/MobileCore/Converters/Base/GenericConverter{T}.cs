using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.IoC;

namespace MobileCore.Droid
{
    [MvxUnconventional]
    public class GenericConverter<TFrom, TParameter, TTo> : MvxValueConverter<TFrom, TTo>
    {
        private readonly Func<TFrom, TParameter, TTo> convertFunction;
        private readonly Func<TTo, TParameter, TFrom> convertBackFunction;

        public GenericConverter(Func<TFrom, TParameter, TTo> convertFunction, Func<TTo, TParameter, TFrom> convertBackFunction = null)
        {
            this.convertFunction = convertFunction;
            this.convertBackFunction = convertBackFunction;
        }

        protected override TTo Convert(TFrom value, Type targetType, object parameter, CultureInfo culture)
            => convertFunction == null ? default(TTo) : convertFunction(value, (TParameter)parameter);

        protected override TFrom ConvertBack(TTo value, Type targetType, object parameter, CultureInfo culture)
        {
            return convertBackFunction == null
                ? base.ConvertBack(value, targetType, parameter, culture)
                : convertBackFunction(value, (TParameter)parameter);
        }
    }
}

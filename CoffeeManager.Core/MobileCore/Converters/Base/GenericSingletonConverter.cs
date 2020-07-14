using System;
namespace MobileCore.Droid
{
    public abstract class GenericSingletonConverter<TConverter, TFrom, TTo> : GenericConverter<TFrom, TTo>
        where TConverter : GenericConverter<TFrom, TTo>, new()
    {
        private static TConverter instance;

        public static TConverter Instance => instance ?? (instance = new TConverter());

        protected GenericSingletonConverter(Func<TFrom, TTo> convertFunction)
            : base(convertFunction)
        {
        }
    }
}

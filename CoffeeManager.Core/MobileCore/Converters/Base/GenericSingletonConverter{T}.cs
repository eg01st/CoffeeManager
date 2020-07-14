using System;
namespace MobileCore.Droid
{
    public abstract class GenericSingletonConverter<TConverter, TFrom, TParameter, TTo>
         : GenericConverter<TFrom, TParameter, TTo>
         where TConverter : GenericConverter<TFrom, TParameter, TTo>, new()
    {
        private static TConverter instance;

        protected GenericSingletonConverter(Func<TFrom, TParameter, TTo> convertFunction)
            : base(convertFunction)
        {
        }

        public static TConverter Instance => instance ?? (instance = new TConverter());
    }
}

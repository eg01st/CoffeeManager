using System;
using MvvmCross.Platform.Converters;
namespace CoffeeManagerAdmin.iOS
{
    public class DecimalToStringConverter : GenericConverter<decimal, string>
    {
        public DecimalToStringConverter() : base((decimal arg) => arg.ToString("F"),
                       (string arg) => decimal.Parse(arg))
        {

        }
    }
}

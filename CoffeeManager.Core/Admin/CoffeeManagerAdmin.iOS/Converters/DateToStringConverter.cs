using System;
namespace CoffeeManagerAdmin.iOS
{
    public class DateToStringConverter : GenericConverter<DateTime, string>
    {
        public DateToStringConverter() : base( (arg) => arg.ToString("D"), (arg) => DateTime.Parse(arg))
        {
        }
    }
}

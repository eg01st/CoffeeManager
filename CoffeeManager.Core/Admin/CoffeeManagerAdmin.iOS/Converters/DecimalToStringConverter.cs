namespace CoffeeManagerAdmin.iOS.Converters
{
    public class DecimalToStringConverter : GenericConverter<decimal, string>
    {
        public DecimalToStringConverter() : base((decimal arg) => arg.ToString("F"),
                       (string arg) =>
                       {
                           decimal result;
                           decimal.TryParse(arg, out result);
                           return result;
                       })
        {

        }
    }
}

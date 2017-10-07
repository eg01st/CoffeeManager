using System;
namespace CoffeeManagerAdmin.iOS
{
    public class InvertedBoolConverter : GenericConverter<bool,bool>
    {
        public InvertedBoolConverter() : base((arg) => !arg, (arg) => !arg)
        {
        }
    }
}

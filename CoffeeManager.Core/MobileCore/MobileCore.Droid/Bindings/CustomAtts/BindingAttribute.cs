using System;
using MvvmCross.Binding;

namespace MobileCore.Droid.Bindings.CustomAtts
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class BindingAttribute : Attribute
    {
        public BindingAttribute(string propNameFrom, 
            string propNameTo, 
            MvxBindingMode mode = MvxBindingMode.OneWay,
            string converterName = null)
        {
            propNameFrom = NameFrom;
            propNameTo = NameTo;
            BindingMode = mode;
            ConverterName = converterName;
        }

        public string NameFrom { get; set; }

        public string NameTo { get; set; }

        public MvxBindingMode BindingMode { get; set; }

        public string ConverterName { get; set; }
    }
}
using System;
using Android.Views;

namespace CoffeeManager.Droid.Extensions
{
    public static class ViewListenersExtensions
    {
        public static View.IOnClickListener SetOnClickListener(this View view, Action<View> onClick)
        {
            var listener = new ViewOnClickListener(onClick);
            view.SetOnClickListener(listener);

            return listener;
        }
    }
}

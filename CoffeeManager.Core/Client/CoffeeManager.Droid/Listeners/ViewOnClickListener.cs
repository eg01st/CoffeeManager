using System;
using Android.Views;

namespace CoffeeManager.Droid
{
    public class ViewOnClickListener : Java.Lang.Object, View.IOnClickListener
    {
        private readonly Action<View> onClick;

        public ViewOnClickListener(Action<View> onClick)
        {
            this.onClick = onClick;
        }

        public void OnClick(View v)
        {
            onClick(v);
        }
    }
}

using System;
using Android.Views;
using MobileCore.Extensions;

namespace MobileCore.Droid.Listeners
{
    public class ViewOnClickListener : Java.Lang.Object, View.IOnClickListener
    {
        private readonly Action<View> onClick;

        public ViewOnClickListener(Action<View> onClick)
        {
            onClick.ThrowIfNull(nameof(onClick));

            this.onClick = onClick;
        }

        public void OnClick(View v)
        {
            onClick(v);
        }
    }
}
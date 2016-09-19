using System;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;

namespace CoffeeManager.Droid.Adapters.ViewHolders
{
    public class UserViewHolder : RecyclerView.ViewHolder
    {
        public UserViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public UserViewHolder(View itemView) : base(itemView)
        {
        }
    }
}
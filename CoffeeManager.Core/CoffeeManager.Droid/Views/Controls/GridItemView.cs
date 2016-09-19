using System;
using Android.Content;
using Android.Runtime;
using MvvmCross.Binding.Droid.Views;

namespace CoffeeManager.Droid.Views.Controls
{
    public class GridItemView : MvxListItemView
    {
        public GridItemView(Context context, IMvxLayoutInflaterHolder layoutInflater, object dataContext, int templateId)
			: base(context, layoutInflater, dataContext, templateId)
		{
        }

        public GridItemView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
        }
    }
}
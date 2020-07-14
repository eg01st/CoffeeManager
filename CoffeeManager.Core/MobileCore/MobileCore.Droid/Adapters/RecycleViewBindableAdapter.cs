using System;
using Android.Support.V7.Widget;
using Android.Views;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Extensions;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MobileCore.Droid.Adapters
{
    public class RecycleViewBindableAdapter : MvxRecyclerAdapter
    {
        public RecycleViewBindableAdapter()
        {
        }

        public RecycleViewBindableAdapter(IMvxAndroidBindingContext bindingContext) : base(bindingContext)
        {
        }

        public RecycleViewBindableAdapter(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);

            if (!(ItemTemplateSelector is ITemplateSelector))
            {
                throw new Exception("ItemTemplateSelector must implement ITemplateSelector");
            }

            var templateSelector = (ITemplateSelector)ItemTemplateSelector;

            var viewHolder = Activator.CreateInstance(templateSelector.GetItemViewHolderType(viewType), itemBindingContext.BindingInflate(templateSelector.GetItemLayoutId(viewType), parent, false), itemBindingContext) as MvxRecyclerViewHolder;
            viewHolder.ThrowIfNull(nameof(viewHolder));

            viewHolder.Click = ItemClick;

            return viewHolder;
        }
    }
}
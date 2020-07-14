using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using MobileCore.Droid.Listeners;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MobileCore.Droid.Controls
{
    [Register("EndlessRecyclerView")]
    public class EndlessRecyclerView : MvxRecyclerView
    {
        public const int DefaultStyle = -1;

        private EndlessScrollChangedListener scrollChangedListener;
        private OnLoadMoreListener onLoadMoreListener;
        private bool hasNextPage = false;
        private LinearLayoutManager linearLayoutManager;

        protected EndlessRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public EndlessRecyclerView(Context context) : this(context, null)
        {
        }

        public EndlessRecyclerView(Context context, IAttributeSet attrs)
            : this(context, attrs, DefaultStyle)
        {
        }

        public EndlessRecyclerView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Init(context, attrs, defStyle);
        }

        public bool HasNextPage
        {
            get { return hasNextPage; }
            set
            {
                if (hasNextPage == value)
                {
                    return;
                }

                hasNextPage = value;
                if (value == true)
                {
                    AddOnScrollListener(scrollChangedListener);
                }
                else
                {
                    RemoveOnScrollListener(scrollChangedListener);
                }
            }
        }

        public void SetOnLoadMoreListener(OnLoadMoreListener onLoadMoreListener)
        {
            this.onLoadMoreListener = onLoadMoreListener;
        }

        private void Init(Context context, IAttributeSet attrs, int defStyle)
        {
            scrollChangedListener = new EndlessScrollChangedListener();
            scrollChangedListener.SetOnLoadMoreListener(new OnLoadMoreListener(OnLoadMore));
            
            linearLayoutManager = new LinearLayoutManager(context, LinearLayoutManager.Vertical, false);
            SetLayoutManager(linearLayoutManager);
        }

        private void OnLoadMore()
        {
            onLoadMoreListener?.OnLoadMore();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                SetOnScrollChangeListener(null);
                RemoveOnScrollListener(scrollChangedListener);
                onLoadMoreListener = null;
                scrollChangedListener?.SetOnLoadMoreListener(null);
                scrollChangedListener = null;
            }

            base.Dispose(disposing);
        }
    }
}
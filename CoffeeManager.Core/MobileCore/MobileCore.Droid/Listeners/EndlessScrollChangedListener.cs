using System;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;

namespace MobileCore.Droid.Listeners
{
    public class EndlessScrollChangedListener : RecyclerView.OnScrollListener
    {
        private OnLoadMoreListener onLoadMoreListener;

        private int scrolledDx = 0;
        private int scrolledDy = 0;

        public EndlessScrollChangedListener()
            : base()
        {
        }

        public EndlessScrollChangedListener(IntPtr ptr, JniHandleOwnership owner)
            : base(ptr, owner)
        {
        }

        public void SetOnLoadMoreListener(OnLoadMoreListener onLoadMoreListener)
        {
            this.onLoadMoreListener = onLoadMoreListener;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            var oldDx = scrolledDx;
            var oldDy = scrolledDy;
            scrolledDx += dx;
            scrolledDy += dy;
            OnScrollChange(recyclerView, scrolledDx, scrolledDy, oldDx, oldDy);
        }

        public void OnScrollChange(NestedScrollView v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
            OnScrollChange((View)v, scrollX, scrollY, oldScrollX, oldScrollY);
        }

        public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
        {
            var absScrollY = Math.Abs(scrollY);
            if ((absScrollY >= v.MeasuredHeight / 2)
                && (absScrollY > oldScrollY))
            {
                onLoadMoreListener?.OnLoadMore();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                onLoadMoreListener = null;
            }

            base.Dispose(disposing);
        }
    }
}
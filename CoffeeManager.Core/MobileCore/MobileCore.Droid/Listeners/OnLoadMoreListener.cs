using System;
using MobileCore.Extensions;

namespace MobileCore.Droid.Listeners
{
    public class OnLoadMoreListener
    {
        private readonly Action onLoadMore;

        public OnLoadMoreListener(Action onLoadMore)
        {
            onLoadMore.ThrowIfNull(nameof(onLoadMore));

            this.onLoadMore = onLoadMore;
        }

        public void OnLoadMore()
        {
            onLoadMore();
        }
    }
}
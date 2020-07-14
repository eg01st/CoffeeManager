using MobileCore.Droid.Controls;
using MobileCore.Droid.Listeners;

namespace MobileCore.Droid.Bindings
{
    public class EndlessListLoadMoreTargetBinding : BaseCommandTargetBinding<EndlessRecyclerView>
    {
        public EndlessListLoadMoreTargetBinding(EndlessRecyclerView targetObject)
            : base(targetObject)
        {
        }

        protected override void SubscribeToTargetEvents(EndlessRecyclerView target)
        {
            var onLoadMoreListener = new OnLoadMoreListener(() => Execute(null));
            target.SetOnLoadMoreListener(onLoadMoreListener);
        }

        protected override void UnsubscribeFromTargetEvents(EndlessRecyclerView target)
        {
            target.SetOnLoadMoreListener(null);
        }
    }
}
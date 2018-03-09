using Android.Views;
using Android.Widget;

namespace CoffeeManager.Droid.Extensions
{
    public static class Utils
    {
        public static void SetDynamicHeight(GridView grid)
        {
            var adapter = grid.Adapter;
            if (adapter == null)
            {
                return;
            }

            int height = 0;
            int desiredWidth = View.MeasureSpec.MakeMeasureSpec(grid.Width, MeasureSpecMode.Unspecified);
            for (int i = 0; i < adapter.Count; i++)
            {
                var listItem = adapter.GetView(i, null, grid);
                listItem.Measure(desiredWidth, (int)MeasureSpecMode.Unspecified);
                height += listItem.MeasuredHeight;
            }

            var param = grid.LayoutParameters;
            param.Height = height + (grid.VerticalSpacing * (adapter.Count - 1)) ;
            grid.LayoutParameters = param;
            grid.RequestLayout();
        }
    }
}
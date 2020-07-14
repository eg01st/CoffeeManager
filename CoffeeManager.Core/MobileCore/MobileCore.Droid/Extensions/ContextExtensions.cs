using Android.Content;

namespace MobileCore.Droid.Extensions
{
    public static class ContextExtensions
    {
        public static TService GetSystemService<TService>(this Context ctx, string name)
            where TService : Java.Lang.Object
            => ctx.GetSystemService(name) as TService;
    }
}
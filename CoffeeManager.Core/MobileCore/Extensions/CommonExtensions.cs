using System.Collections.ObjectModel;

namespace MobileCore.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Remove elements one by one
        /// </summary>
        /// <see href="https://forums.xamarin.com/discussion/19114/invalid-number-of-rows-in-section"/>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        public static void SafeClear<T>(this ObservableCollection<T> collection)
        {
            for(var i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }
        }

        public static bool IsNullOrDefault<T>(this T value)
        {   
            return Equals(value, default(T));
        }
    }
}
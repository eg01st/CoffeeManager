using System;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.Views;
using MobileCore.Extensions;

namespace MobileCore.Droid.Bindings.CustomAtts
{
    public static class ViewBinder
    {
        public static Unbinder Bind(this View view, object target) => 
            Bind(target, viewId => view.FindViewById(viewId));

        public static Unbinder Bind(this Activity activity) =>
            Bind(activity, viewId => activity.FindViewById(viewId));

        public static Unbinder Bind(object target, Func<int, View> findById)
        {
            var unbinder = new Unbinder(target);
            var activityType = target.GetType();

            var memeberFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var fields = activityType.GetFields(memeberFlags);
            var properties = activityType.GetProperties(memeberFlags);

            var members = fields.Concat<MemberInfo>(properties);

            foreach (var member in members)
            {
                Attribute.GetCustomAttributes(member, typeof(FindByIdAttribute))
                    .OfType<FindByIdAttribute>()
                    .FirstOrDefault()
                    .With(findByIdAttribute =>
                    {
                        member.SetValue(target, findById(findByIdAttribute.ViewId));
                        unbinder.AddBinding(member);
                    });
            }

            return unbinder;
        }
    }
}
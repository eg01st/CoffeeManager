using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileCore.Extensions
{
    public static class Maybe
    {
        public static bool IsNull<T>(this T? o)
            where T : struct
        {
            return o == null;
        }

        public static bool IsNull<TInput>(this TInput o)
            where TInput : class
        {
            return o == null;
        }

        public static bool IsNotNull<TInput>(this TInput o)
            where TInput : class
        {
            return o != null;
        }

        public static bool IsNullOrEmpty<TInput>(this IEnumerable<TInput> o)
        {
            return o == null || !o.Any();
        }

        public static bool IsNotNullNorEmpty<TInput>(this IEnumerable<TInput> o)
        {
            return o?.Any() ?? false;
        }

        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }

        public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null)
            {
                return null;
            }

            return evaluator(o) ? o : null;
        }

        public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (o == null)
            {
                return null;
            }

            return evaluator(o) ? null : o;
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action)
            where TInput : class
        {
            if (o == null)
            {
                return null;
            }

            action(o);
            return o;
        }
    }
}

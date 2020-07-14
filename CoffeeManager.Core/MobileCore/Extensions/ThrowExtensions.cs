using System;

namespace MobileCore.Extensions
{
    public static class ThrowExtensions
    {
        public static void ThrowIfNullOrEmpty(this string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Parameter is null or empty", paramName);
            }
        }

        public static void ThrowIfNullOrEmpty(this string str, string paramName, string message)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message, paramName);
            }
        }

        public static void ThrowIfNullOrEmpty(this string str, string message, Exception innerException)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message, innerException);
            }
        }

        public static void ThrowIfNullOrEmpty(this string str, string message, string paramName, Exception innerException)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException(message, paramName, innerException);
            }
        }

        public static void ThrowIfNull(this object o, string paramName)
        {
            if (o == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIfNull(this object o, string paramName, string message)
        {
            if (o == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void ThrowIfNull(this object o, string message, Exception innerException)
        {
            if (o == null)
            {
                throw new ArgumentNullException(message, innerException);
            }
        }
    }
}

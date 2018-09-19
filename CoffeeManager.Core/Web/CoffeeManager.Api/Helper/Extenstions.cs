using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CoffeeManager.Api.Helper
{
    public static class Extenstions
    {
        public static decimal GetPercentValueOf(this decimal body, decimal percent)
        {
            return body / 100 * percent;
        }

        public static StringBuilder ToDiagnosticString(this Exception exception, StringBuilder stringBuilder)
        {
            for (var ex = exception; ex != null; ex = ex.InnerException)
            {
                var exceptionText = $"Exception: {ex.GetType()}";

                if (stringBuilder.Length != 0)
                {
                    stringBuilder
                        .Append('-', exceptionText.Length)
                        .AppendLine();
                }

                stringBuilder.AppendLine(exceptionText);

                if (!string.IsNullOrEmpty(ex.Message))
                {
                    stringBuilder.AppendLine(ex.Message);
                }

                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    stringBuilder.AppendLine(ex.StackTrace);
                }

                var aex = ex as AggregateException;

                if (aex?.InnerExceptions != null)
                {
                    var foundInnerException = false;

                    foreach (var e in aex.InnerExceptions)
                    {
                        foundInnerException = foundInnerException || e != ex.InnerException;
                        ToDiagnosticString(e, stringBuilder);
                    }

                    if (foundInnerException)
                    {
                        ex = ex.InnerException;
                    }
                }
            }

            return stringBuilder;
        }

        public static string ToDiagnosticString(this Exception exception)
            => exception.ToDiagnosticString(new StringBuilder()).ToString();
    }
}
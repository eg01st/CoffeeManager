using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using MobileCore.Extensions;
using MobileCore.Logging;

namespace MobileCore
{
    public static class ExceptionExtensions
    {
        private static readonly string[] IgnoredCallStackStrings =
        {
            "End of stack trace from previous location where exception was thrown",
            "System.Runtime.ExceptionServices.",
            "System.Runtime.CompilerServices."
        };

        //for exception logging optimiztion
        [Conditional("DEBUG")]
        public static void OptimizedLogToConsole(this Exception e)
        {
            ConsoleLogger.Error(e.ToDiagnosticString());
        }

        public static void LogToConsole(this Exception e)
        {
            ConsoleLogger.Exception(e);
        }

        /// <summary>
        /// Returns detailed exception text
        /// </summary>
        /// <param name="exception">Exception to process.</param>
        /// <param name="stringBuilder"><see cref="StringBuilder"/> instance.</param>
        /// <returns>Detailed exception text.</returns>
        public static StringBuilder ToDiagnosticString(this Exception exception, StringBuilder stringBuilder, int indentationLevel = 0)
        {
            for (var ex = exception; ex != null; ex = ex.InnerException)
            {
                var exceptionText = $"Exception: {ex.GetType()}";

                if (stringBuilder.Length != 0)
                {
                    stringBuilder.AppendLine("".PadLeft(exceptionText.Length, '-'), indentationLevel);
                }

                stringBuilder.AppendLine(exceptionText, indentationLevel);

                if (!string.IsNullOrEmpty(ex.Message))
                {
                    stringBuilder.AppendLine(ex.Message, indentationLevel);
                }

                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    var lines = ex.StackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var stackLines = lines.Where(line => !IgnoredCallStackStrings.Any(line.Contains));
                    foreach (var line in stackLines)
                    {
                        stringBuilder.AppendLine(line, indentationLevel);
                    }
                }

                var bdx = ex as BusinessDomainException;

                if (bdx.IsNotNull())
                {
                    stringBuilder.AppendLine(bdx.BuildExceptionString());
                }

                var aex = ex as AggregateException;
                if (aex?.InnerExceptions != null)
                {
                    var foundInnerException = false;

                    foreach (var e in aex.InnerExceptions)
                    {
                        foundInnerException = foundInnerException || e != ex.InnerException;
                        ToDiagnosticString(e, stringBuilder, ++indentationLevel);
                        --indentationLevel;
                    }

                    if (foundInnerException)
                    {
                        ex = ex.InnerException;
                    }
                }
            }

            return stringBuilder;
        }

        /// <summary>
        /// Returns detailed exception text
        /// </summary>
        /// <param name="exception">Exception to process.</param>
        /// <returns>Detailed exception text.</returns>
        public static string ToDiagnosticString(this Exception exception)
            => exception.ToDiagnosticString(new StringBuilder()).ToString();

        public static string ToErrorReportEmailBody(this Exception exception)
        {
            var emailBuilder = new StringBuilder();
            emailBuilder.AppendLine().AppendLine(); //To give user space to write something if they choose
            emailBuilder.AppendLine("--- Error Details ---");
            emailBuilder = exception.ToDiagnosticString(emailBuilder);

            return emailBuilder.ToString();
        }

        /// <summary>
        /// Throws inner exception with call stack from current exception
        /// Can be used for retrowing inner exception from AggregateException or etc
        /// </summary>
        /// <param name="exception"></param>
        /// <exception cref="Exception"></exception>
        public static void ThrowInner(this Exception exception)
        {
            if (exception.InnerException == null)
            {
                throw exception;
            }

            var edi = ExceptionDispatchInfo.Capture(exception.InnerException);
            edi.Throw();
        }
    }
}

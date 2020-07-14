using System;
using System.Runtime.CompilerServices;
using MobileCore.Extensions;
using MvvmCross.Platform;

namespace MobileCore.Logging
{
    public static class DiagnosticLogger
    {
        public static LogLevel LogLevel { get; set; } = LogLevel.None;
        public static bool Enabled => LogLevel != LogLevel.None;

        private static IDiagnosticLogger logger;
        private static IDiagnosticLogger Logger
        {
            get
            {
                if (logger.IsNull() && Mvx.CanResolve<IDiagnosticLogger>())
                {
                    try
                    {
                        logger = Mvx.Resolve<IDiagnosticLogger>();
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Cannot resolve IConsoleLogger, initialise logger before using logger", ex);
                    }
                }

                return logger;
            }
        }

        public static void Error(string message,
                                 [CallerMemberName] string memberName = "",
                                 [CallerFilePath] string filePath = "",
                                 [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel < LogLevel.Error)
            {
                return;
            }

            var buildedMessage = BuildMessage(message, filePath, memberName, lineNumber);

            SafeExecute(() => Logger?.Error(buildedMessage));
        }

        public static void Error(Exception exception,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel < LogLevel.Error)
            {
                return;
            }

            var exceptionMessage = exception.ToDiagnosticString();
            var buildedMessage = BuildMessage(exceptionMessage, filePath, memberName, lineNumber);

            SafeExecute(() => Logger?.Error(buildedMessage));
        }

        public static void Trace(string message,
                                 [CallerMemberName] string memberName = "",
                                 [CallerFilePath] string filePath = "",
                                 [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel < LogLevel.Info)
            {
                return;
            }

            var buildedMessage = BuildMessage(message, filePath, memberName, lineNumber);

            SafeExecute(() => Logger?.Trace(buildedMessage));
        }

        public static void Warning(string message,
                                   [CallerMemberName] string memberName = "",
                                   [CallerFilePath] string filePath = "",
                                   [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel < LogLevel.Warning)
            {
                return;
            }

            var buildedMessage = BuildMessage(message, filePath, memberName, lineNumber);

            SafeExecute(() => Logger?.Warning(buildedMessage));
        }

        private static void SafeExecute(Action logAction)
        {
            try
            {
                logAction();
            }
            catch (Exception)
            {
                // what else can we do?
            }
        }

        private static string BuildMessage(string message, string filePath, string memberName, int lineNumber)
        {
            return $"{DateTime.Now:s} : {filePath} {memberName}:{lineNumber} ' {message} '";
        }
    }
}

using System;
using MobileCore.Extensions;
using MvvmCross.Platform;

namespace MobileCore.Logging
{
    public static class ConsoleLogger
    {
        private static IConsoleLogger logger;
        private static IConsoleLogger Logger
        {
            get
            {
                if (logger.IsNull() && Mvx.CanResolve<IConsoleLogger>())
                {
                    try
                    {
                        logger = Mvx.Resolve<IConsoleLogger>();
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Cannot resolve IConsoleLogger, initialise logger before using logger", ex);
                    }
                }

                return logger;
            }
        }

        public static void Exception(Exception e)
        {
            Error(e.ToDiagnosticString());
        }

        public static void Error(string message)
        {
            SafeExecute(() => Logger?.Error(message));
        }

        public static void Trace(string message)
        {
            SafeExecute(() => Logger?.Trace(message));
        }

        public static void Warning(string message)
        {
            SafeExecute(() => Logger?.Warning(message));
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
    }
}

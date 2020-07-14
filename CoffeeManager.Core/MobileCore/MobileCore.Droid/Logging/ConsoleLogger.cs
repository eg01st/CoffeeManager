using System;
using MobileCore.Extensions;
using MobileCore.Logging;

namespace MobileCore.Droid.Logging
{
    public class ConsoleLogger : IConsoleLogger
    {
        private readonly string tag;

        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        public ConsoleLogger(string tag)
        {
            tag.ThrowIfNullOrEmpty("tag");

            this.tag = tag;
        }

        public void Exception(Exception e)
        {
            var message = e.ToDiagnosticString();
            Error(message);
        }

        public void Error(string message)
        {
            if (LogLevel < LogLevel.Error)
            {
                return;
            }

            Android.Util.Log.Error(tag, message);
        }

        public void Trace(string message)
        {
            if (LogLevel < LogLevel.Info)
            {
                return;
            }

            Android.Util.Log.Debug(tag, message);
        }

        public void Warning(string message)
        {
            if (LogLevel < LogLevel.Warning)
            {
                return;
            }

            Android.Util.Log.Warn(tag, message);
        }
    }
}

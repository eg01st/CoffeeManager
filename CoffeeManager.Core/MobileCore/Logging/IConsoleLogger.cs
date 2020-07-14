using System;

namespace MobileCore.Logging
{
    public interface IConsoleLogger
    {
        LogLevel LogLevel { get; set; }
        void Error(string message);
        void Exception(Exception e);
        void Trace(string message);
        void Warning(string message);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeeManager.Api.Helper;
using NLog;

namespace CoffeeManager.Api
{
    public static class Log
    {
        private static readonly Logger Logger = LogManager.GetLogger("Global");

        public static void Error(Exception ex, string mesasge = "")
        {
            Logger.Error($"{mesasge} {ex.ToDiagnosticString()}");
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warn(string message)
        {
            Logger.Warn(message);
        }
    }
}
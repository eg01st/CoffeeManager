using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeeManager.Api.Helper;
using NLog;
using NLog.Targets;

namespace CoffeeManager.Api
{
    public static class Log
    {
        private static readonly Logger Logger = LogManager.GetLogger("Global");

        static Log()
        {
            //var target = (FileTarget)LogManager.Configuration.FindTargetByName("file");
            //string root = HttpContext.Current.Server.MapPath("~/App_Data");
            //string fullName = root + "/log/log.txt";
            //target.FileName = fullName;
            //LogManager.ReconfigExistingLoggers();
        }

        public static void Error(Exception ex, string mesasge = "")
        {
            //Logger.Error($"{mesasge} {ex.ToDiagnosticString()}");
        }

        public static void Info(string message)
        {
            //Logger.Info(message);
        }

        public static void Warn(string message)
        {
            //Logger.Warn(message);
        }
    }
}
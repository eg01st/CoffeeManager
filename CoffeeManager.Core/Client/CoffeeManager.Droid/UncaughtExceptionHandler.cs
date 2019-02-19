using System;
using Android.Content;
using Android.Runtime;
using CoffeeManager.Common;
using Java.Lang;
using MobileCore;
using MobileCore.Email;
using MvvmCross.Platform;
using StringBuilder = System.Text.StringBuilder;

namespace CoffeeManager.Droid
{
    public class UncaughtExceptionHandler : Java.Lang.Object, Thread.IUncaughtExceptionHandler, IJavaObject, IDisposable
    {
        public const string True = "1";
        public const string False = "0";
        public readonly Context Context;

        public UncaughtExceptionHandler(Context context)
        {
            Context = context;
        }

        public void UncaughtException(Thread thread, Throwable ex)
        {
            var daemon = thread.Daemon ? True : False;
            var alive = thread.IsAlive ? True : False;
            var interrupted = thread.IsInterrupted ? True : False;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"thread id: {thread.Id}; thread name: {thread.Name}; thread priority: {thread.Priority}\n");
            stringBuilder.Append($"is daemon: {daemon}; is alive: {alive}; is interrupted: {interrupted}");

            var message = stringBuilder.ToString();
            var exceptionTolog = new System.Exception(message, ex);

            Mvx.Resolve<IEmailService>().SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",exceptionTolog.ToDiagnosticString());
        }
    }
}
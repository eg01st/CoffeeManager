using System;
using System.Globalization;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using Android.Widget;
using MailKit.Net.Smtp;
using MimeKit;
using MvvmCross.Platform;
using CoffeManager.Common;
using CoffeeManager.Common;

namespace CoffeeManager.Droid
{
    [Application]
    public class DroidApplication : Application
    {
        public DroidApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentCulture;
        }


        public override void OnCreate()
        {
            base.OnCreate();
            UserDialogs.Init(this);
            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += OnAndroidEnvironmentUnhandledExceptionRaiser;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
            Java.Lang.Thread.DefaultUncaughtExceptionHandler = new UncaughtExceptionHandler(Context);
        }

        private static void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exceptionObject = e.ExceptionObject;
            var exception = exceptionObject as Exception;
            if (exception != null)
            {
                ReportException(exception);
            }
            else
            {
                Mvx.Resolve<IEmailService>().SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",exceptionObject.ToString());
            }
        }

        private static void OnAndroidEnvironmentUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            var exception = e.Exception;
            ReportException(exception);
            e.Handled = true;
        }

        private static void OnTaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ReportException(e.Exception);
        }

        private static void ReportException(Exception e)
        {
            if (e == null)
            {
                return;
            }
            Mvx.Resolve<IUserDialogs>().Alert("Что-то пошло не так :(");
            Mvx.Resolve<IEmailService>().SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}", e.ToDiagnosticString());
        }
    }
}
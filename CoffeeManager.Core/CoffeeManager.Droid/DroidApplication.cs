using System;
using System.Globalization;
using System.Threading.Tasks;
using Android.App;
using Android.Runtime;
using MailKit.Net.Smtp;
using MimeKit;

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
                SendMessage(exceptionObject.ToString());
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
           SendMessage(e.ToString());
        }

        public static void SendMessage(string mes)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Info", "serdechnyi.dima@gmail.com"));
            message.To.Add(new MailboxAddress("", "tertyshnykov@gmail.com"));
            message.Subject = "Error";

            message.Body = new TextPart("plain")
            {
                Text = mes
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("serdechnyi.dima@gmail.com", "");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
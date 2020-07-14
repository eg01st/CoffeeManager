using System;
using Foundation;
using MobileCore.Logging;
using MobileCore.Extensions;


namespace MobileCore.iOS
{
    public class ConsoleLogger : IConsoleLogger
    {
        private const string FoundationLibrary = "/System/Library/Frameworks/Foundation.framework/Foundation";

        [System.Runtime.InteropServices.DllImport(FoundationLibrary)]
        private static extern void NSLog(IntPtr format, IntPtr s);

        [System.Runtime.InteropServices.DllImport(FoundationLibrary, EntryPoint = "NSLog")]
        private static extern void NSLog_ARM64(IntPtr format, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr s);

        private static readonly bool Is64Bit = IntPtr.Size == 8;
        private static readonly bool IsDevice = ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.DEVICE;
        private static readonly bool Is64BitDevice = Is64Bit && IsDevice;
        private static readonly NSString NsFormat = new NSString(@"%@");

        private readonly string tag;

        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        public ConsoleLogger(string tag)
        {
            tag.ThrowIfNullOrEmpty("tag");

            this.tag = tag;
        }

        public void Exception(Exception e)
        {
            if (LogLevel < LogLevel.Error)
            {
                return;
            }

            var message = BuildMessage(tag, "Exception", e.ToDiagnosticString());
            WriteToNativeLog(message);
        }

        public void Error(string message)
        {
            if (LogLevel < LogLevel.Error)
            {
                return;
            }

            message = BuildMessage(tag, "Error", message);
            WriteToNativeLog(message);
        }

        public void Trace(string message)
        {
            if (LogLevel < LogLevel.Info)
            {
                return;
            }

            message = BuildMessage(tag, "Debug", message);
            WriteToNativeLog(message);
        }

        public void Warning(string message)
        {
            if (LogLevel < LogLevel.Warning)
            {
                return;
            }

            message = BuildMessage(tag, "Warning", message);
            WriteToNativeLog(message);
        }

        private static string BuildMessage(string tag, string logType, string text)
        {
            var message = $"{tag} {logType}:\t{text}";
            return message;
        }

        protected static void WriteToNativeLog(string text)
        {
            using (var nsText = new NSString(text))
            {
                WriteToNativeLog(nsText);
            }
        }

        protected static void WriteToNativeLog(NSString text)
        {
            if (Is64BitDevice)
            {
                NSLog_ARM64(NsFormat.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, text.Handle);
                return;
            }

            NSLog(NsFormat.Handle, text.Handle);
        }
    }
}

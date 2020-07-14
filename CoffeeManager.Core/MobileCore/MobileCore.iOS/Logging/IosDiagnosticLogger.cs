using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCore.Extensions;
using Foundation;
using MobileCore.Logging;

namespace MobileCore.iOS
{
    public class IosDiagnosticLogger : IDiagnosticLogger
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

        private const string DiagnosticDirectoryName = "DiagnosticLogs";

        private readonly string tag;
        private readonly StreamWriter streamWriter;

        public static int SavedDays { get; set; } = 2;

        public string DirectoryLogsPath { get; private set; }

        public IosDiagnosticLogger(string tag)
        {
            tag.ThrowIfNullOrEmpty("tag");

            this.tag = tag;

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var diagnosticDirectory = Path.Combine(documents, DiagnosticDirectoryName);
            if (!Directory.Exists(diagnosticDirectory))
            {
                Directory.CreateDirectory(diagnosticDirectory);
            }

            DirectoryLogsPath = diagnosticDirectory;
            DeleteEarlyLogFiles(diagnosticDirectory);

            var dateString = DateTime.Now.ToString("MM-dd-yyyy");
            var dateDirectory = Path.Combine(diagnosticDirectory, dateString);
            if (!Directory.Exists(dateDirectory))
            {
                Directory.CreateDirectory(dateDirectory);
            }

            var nowTimeString = DateTime.Now.ToString("MM-dd-yyyy--HH-mm-ss");
            var filename = Path.Combine(dateDirectory, $"{nowTimeString}.log");

            streamWriter = new StreamWriter(File.OpenWrite(filename), Encoding.Unicode);
            streamWriter.AutoFlush = true;
        }

        protected virtual void DeleteEarlyLogFiles(string directoryPath)
        {
            var notNeedCleanedDayDate = DateTime.Now.AddDays(-SavedDays);
            var directories = Directory.GetDirectories(directoryPath);

            foreach (var directory in directories)
            {
                var directoryName = Path.GetFileName(directory);

                DateTime directoryDate;
                if (!DateTime.TryParse(directoryName, out directoryDate))
                {
                    continue;
                }

                if (directoryDate < notNeedCleanedDayDate)
                {
                    Directory.Delete(directory, true);
                }
            }
        }

        public void Error(string message)
        {
            message = BuildMessage(tag, "Error", message);
            WriteToNativeLog(message);
            streamWriter.Write(message);
        }

        public void Trace(string message)
        {
            message = BuildMessage(tag, "Debug", message);
            WriteToNativeLog(message);
            streamWriter.Write(message);
        }

        public void Warning(string message)
        {
            message = BuildMessage(tag, "Warning", message);
            WriteToNativeLog(message);
            streamWriter.Write(message);
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

        public async Task<byte[]> GetLogsArchiveContentAsync()
        {
            var content = await Task.Run(() => GetLogsArchiveContent());
            return content;
        }

        protected virtual byte[] GetLogsArchiveContent()
        {
            var folders = Directory.GetDirectories(DirectoryLogsPath);
            var tmpDirectory = Path.Combine(DirectoryLogsPath, "tmp");
            if (Directory.Exists(tmpDirectory))
            {
                Directory.Delete(tmpDirectory, true);
            }

            var filesToCopy = folders
                .SelectMany(Directory.GetFiles)
                .Select(x => new { NewFilePath = Path.Combine(tmpDirectory, Path.GetFileName(x)), FilePath = x })
                .ToArray();

            Directory.CreateDirectory(tmpDirectory);
            foreach (var file in filesToCopy)
            {
                File.Copy(file.FilePath, file.NewFilePath, true);
            }

            var zipPath = Path.Combine(DirectoryLogsPath, "Logs.zip");
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            ZipFile.CreateFromDirectory(tmpDirectory, zipPath);

            var zipContent = File.ReadAllBytes(zipPath);

            Directory.Delete(tmpDirectory, true);
            File.Delete(zipPath);

            return zipContent;
        }
    }
}

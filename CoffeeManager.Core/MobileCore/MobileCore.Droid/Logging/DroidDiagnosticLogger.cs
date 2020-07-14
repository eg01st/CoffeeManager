using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCore.Extensions;
using MobileCore.Logging;

namespace MobileCore.Droid.Logging
{
    public class DroidDiagnosticLogger : IDiagnosticLogger
    {
        private readonly string tag;
        private readonly StreamWriter streamWriter;
        private const string DiagnosticDirectoryName = "DiagnosticLogs";
        public static int SavedDays { get; set; } = 2;

        public string DirectoryLogsPath { get; private set; }

        public DroidDiagnosticLogger(string tag)
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
            Android.Util.Log.Error(tag, message);
            streamWriter.Write(message);
        }

        public void Trace(string message)
        {
            Android.Util.Log.Debug(tag, message);
            streamWriter.Write(message);
        }

        public void Warning(string message)
        {
            Android.Util.Log.Warn(tag, message);
            streamWriter.Write(message);
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

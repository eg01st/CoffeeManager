using System;
using System.Threading.Tasks;

namespace MobileCore.Logging
{
    public interface IDiagnosticLogger
    {
        string DirectoryLogsPath { get; }
        void Error(string message);
        void Trace(string message);
        void Warning(string message);

        /// <summary>
        /// Returns byte content of zip file with all available logs
        /// </summary>
        /// <returns>The logs content async.</returns>
        Task<byte[]> GetLogsArchiveContentAsync();
    }
}

using System;
using System.Threading.Tasks;

namespace MobileCore.AutoUpdate
{
    public interface IUpdateProvider
    {
        string DroidAppVersionUrl { get; }
        string DroidPackageUrl { get; }
        Task<int> GetCurrentAppVersion();
        Task<int> GetNewestAppVersion();
        Task<bool> UpdateIsDownloaded();
        Task DownloadUpdate();
        Task UpdateApplication();
        void ConfigureEndpoints(string baseAddess, string droidAppVersionUrl, string droidPackageUrl);
    }
}

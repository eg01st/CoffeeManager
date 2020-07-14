using System.Threading.Tasks;
using MobileCore.Logging;

namespace MobileCore.AutoUpdate
{
    public class UpdateAppWorker : IUpdateAppWorker
    {
        readonly IUpdateProvider updateProvider;

        public UpdateAppWorker(IUpdateProvider updateProvider)
        {
            this.updateProvider = updateProvider;
        }
        

        public void ConfigureEndpoints(string baseAddress, string droidAppVersionUrl, string droidPackageUrl)
        {
            updateProvider.ConfigureEndpoints(baseAddress, droidAppVersionUrl, droidPackageUrl);
        }

        public async Task<bool> IsNewVersionAvailable()
        {
            var fileIsDownloaded = await updateProvider.UpdateIsDownloaded();
            bool isNewVersionAvailableOnWebServer = await IsNewVersionAvailableOnWebServer();
            if (!fileIsDownloaded)
            {
                updateProvider.DownloadUpdate();
            }
            return isNewVersionAvailableOnWebServer && fileIsDownloaded;
        }

        public async Task Update()
        {
            if (await IsNewVersionAvailable())
            {
                ConsoleLogger.Trace("Updating app");
                await updateProvider.UpdateApplication();
            }
        }

        private async Task<bool> IsNewVersionAvailableOnWebServer()
        {
            var currentVersion = await updateProvider.GetCurrentAppVersion();
            var newestVersion = await updateProvider.GetNewestAppVersion();
            return currentVersion < newestVersion;
        }
    }
}

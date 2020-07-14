using System;
using System.Threading.Tasks;

namespace MobileCore.AutoUpdate
{
    public interface IUpdateAppWorker
    {
        Task<bool> IsNewVersionAvailable();

        Task Update();

        void ConfigureEndpoints(string baseAddress, string droidAppVersionUrl, string droidPackageUrl);
    }
}

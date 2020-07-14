using System;
using System.Net.Http;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Java.IO;
using MobileCore.AutoUpdate;
using MobileCore.Email;
using MobileCore.Logging;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace MobileCore.Droid.AutoUpdate
{
    public class UpdateProvider : IUpdateProvider
    {
//        private string versionUrl = "http://185.191.177.62:8083/api/update/getcurrentversion";
//        private string newVersionUrl = "http://185.191.177.62:8083/api/update/GetAndroidPackage";

        string fileName = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads) + "/CoffeeManager.Droid.{0}.apk";
        
        private string baseAddress;
        
        private bool isConfigured = false;
        private volatile bool isDownloadingUpdate;
        private Activity activity => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        private Context context => activity.BaseContext;

        public string DroidAppVersionUrl { get; private set; }
        public string DroidPackageUrl { get; private set; }
        
        public void ConfigureEndpoints(string baseAddress, string droidAppVersionUrl, string droidPackageUrl)
        {
            this.baseAddress = baseAddress;
            DroidAppVersionUrl = droidAppVersionUrl;
            DroidPackageUrl = droidPackageUrl;
            isConfigured = true;
        }
        
        public Task<int> GetCurrentAppVersion()
        {
            if (!isConfigured)
            {
                throw new Exception("Provider should be configured with proper urls");
            }
            var info = context.PackageManager.GetPackageInfo(context.PackageName, 0);
            var version = info.VersionCode;
            return Task.FromResult(version);
        }

        public async Task<int> GetNewestAppVersion()
        {
            if (!isConfigured)
            {
                throw new Exception("Provider should be configured with proper urls");
            }
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new System.Uri(baseAddress);
                var versionString = await client.GetStringAsync(DroidAppVersionUrl);
                var version = int.Parse(versionString);
                return version;
            }
            catch (Exception ex)
            {
                await Mvx.Resolve<IEmailService>().SendErrorEmail("GetNewestAppVersion", ex.ToDiagnosticString());
                ConsoleLogger.Exception(ex);
                return 0;
            }
        }

        public async Task UpdateApplication()
        {
            if (await UpdateIsDownloaded())
            {
                var uri = await GetUpdate();
                if (uri == null)
                {
                    return;
                }
                var install = new Intent(Intent.ActionView);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
                {
                    StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
                    StrictMode.SetVmPolicy(builder.Build());
                }

                install.SetFlags(ActivityFlags.NewTask);
                install.SetDataAndType(uri, "application/vnd.android.package-archive");

                context.StartActivity(install);
            }

        }

        public async Task<bool> UpdateIsDownloaded()
        {
            var latestVersion = await GetNewestAppVersion();
            string fullfileName = string.Format(fileName, latestVersion);
            var file = new File(fullfileName);
            return file.Exists();
        }

        public Task DownloadUpdate()
        {
            return GetUpdate();
        }

        private async Task<Uri> GetUpdate()
        {
            if (!isConfigured)
            {
                throw new Exception("Provider should be configured with proper urls");
            }
            
            var latestVersion = await GetNewestAppVersion();
            string fullfileName = string.Format(fileName, latestVersion);
            
            var file = new File(fullfileName);
            if (file.Exists())
            {
                return Uri.Parse("file://" + fullfileName);
            }

            if(isDownloadingUpdate)
            {
                return null;
            }
            isDownloadingUpdate = true;

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            client.BaseAddress = new System.Uri(baseAddress);
            var bytes = await client.GetByteArrayAsync(DroidPackageUrl);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                var result = await PermissionsManager.RequestPermissionsAsync(activity, true, Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage);
                if (result != PermissionResult.Granted)
                {
                    return null;
                }
            }

            file.CreateNewFile();

            FileOutputStream fos = new FileOutputStream(file.Path);

            fos.Write(bytes);
            fos.Close();

            isDownloadingUpdate = false;

            return Uri.Parse("file://" + fullfileName);
        }
    }
}

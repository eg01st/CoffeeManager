using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.App;
using Android.Support.V4.Content;
using System.Linq;

namespace MobileCore.Droid
{
    public static class PermissionsManager
    {
        public const int PermissionRequestCode = 123;

        private static TaskCompletionSource<PermissionResult> source;

        private static string[] currentPermissions;

        public static async Task<PermissionResult> RequestPermissionsAsync(Activity activity, bool simpleMode = true, params string[] requiredPermissions)
        {
            var missingPermissions = MissingPermissions(activity, requiredPermissions);

            if (!missingPermissions.Any())
            {
                return PermissionResult.Granted;
            }

            if (!simpleMode && RequiresExplanation(activity, requiredPermissions))
            {
                return PermissionResult.ExplainAndRetry;
            }

            currentPermissions = missingPermissions.ToArray();
            source = new TaskCompletionSource<PermissionResult>();

            activity.RunOnUiThread(() => ActivityCompat.RequestPermissions(activity, currentPermissions, PermissionRequestCode));

            return await source.Task;
        }

        private static bool RequiresExplanation(Activity activity, string[] requiredPermissions)
        {
            foreach (var permission in requiredPermissions)
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(activity, permission))
                {
                    return true;
                }
            }

            return false;
        }

        private static List<string> MissingPermissions(Activity activity, string[] requiredPermissions)
        {
            List<string> missingPermissions = new List<string>();

            foreach (var permission in requiredPermissions)
            {
                if (ContextCompat.CheckSelfPermission(activity, permission) != (int)Permission.Granted)
                {
                    missingPermissions.Add(permission);
                }
            }

            return missingPermissions;
        }

        public static void SetPermissionsResult(int requestCode, Permission[] grantResults)
        {
            if (requestCode != PermissionRequestCode)
            {
                return;
            }

            foreach (var grantResult in grantResults)
            {
                if (grantResult != Permission.Granted)
                {
                    source.SetResult(PermissionResult.Declined);

                    return;
                }
            }

            source.SetResult(PermissionResult.Granted);
        }
    }
}

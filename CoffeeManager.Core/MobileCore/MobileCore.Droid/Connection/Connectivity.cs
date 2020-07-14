using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Java.Net;
using MobileCore.Logging;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MobileCore.Connection.Droid
{
	public class Connectivity : ConnectivityBase
	{
		protected static int DefaultPingTimeOut = 1500;

		private readonly ConnectivityChangeBroadcastReceiver receiver;

		private bool disposed;
		private ConnectivityManager connectivityManager;

		public Connectivity()
		{
			try
			{
				ConnectivityChangeBroadcastReceiver.ConnectionChanged = OnConnectivityChanged;
				ConnectivityChangeBroadcastReceiver.ConnectionTypeChanged = OnConnectivityTypeChanged;

				receiver = new ConnectivityChangeBroadcastReceiver();
				Application.Context.RegisterReceiver(receiver, new IntentFilter(ConnectivityManager.ConnectivityAction));

				connectivityManager = (ConnectivityManager)(Application.Context.GetSystemService(Context.ConnectivityService));
			}
			catch (Exception ex)
			{
				ConnectivityChangeBroadcastReceiver.ConnectionChanged = null;
				ConnectivityChangeBroadcastReceiver.ConnectionTypeChanged = null;

				var newException = new ConnectivityException("Exception occured while trying to initialise connectivity manager, See inner exception", ex);
				DiagnosticLogger.Error($"Error during to connectivity initialize \n {newException.ToDiagnosticString()}");
				throw newException;
			}
		}

		/// <summary>
		/// Gets the current connection status
		/// </summary>
		//public override bool HasInternetConnection => IsConnected;

		/// <summary>
		/// Gets the list of all active connection types
		/// </summary>
		public override IEnumerable<ConnectionType> ConnectionTypes => connectivityManager.GetConnectionTypes();

		/// <summary>
		/// Gets current connection state
		/// </summary>
		protected override bool DoGetConnectionStatus()
		{
			return connectivityManager.HasInternetConnection();
		}

		/// <summary>
		/// Check particular host reachability
		/// <param name="hostUrl"></param>
		/// </summary>
        public override bool CheckHostReachability(string hostUrl)
		{
			//if (!IsConnected)
			//{
			//	return false;
			//}

			try
			{
                var host = InetAddress.GetByName(hostUrl);
                var isHostReachable = host.IsReachable(DefaultPingTimeOut);

                return isHostReachable;
			}
			catch (UnknownHostException ex)
			{
				DiagnosticLogger.Error($"Host {hostUrl} cannot be reached with exception: \n {ex.ToDiagnosticString()}");
				return false;
			}
		}


        public async override Task<bool> CheckHostReachabilityAsync(string host)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));

            if (!IsConnected)
                return false;

            host = host.Replace("http://www.", string.Empty).
              Replace("http://", string.Empty).
              Replace("https://www.", string.Empty).
              Replace("https://", string.Empty).
              TrimEnd('/');

            return await Task.Run(async () =>
            {
                try
                {
                    var tcs = new TaskCompletionSource<InetSocketAddress>();
                    new System.Threading.Thread(() =>
                    {
                        /* this line can take minutes when on wifi with poor or none internet connectivity
                        and Task.Delay solves it only if this is running on new thread (Task.Run does not help) */
                        InetSocketAddress result = new InetSocketAddress(host, 80);

                        if (!tcs.Task.IsCompleted)
                            tcs.TrySetResult(result);

                    }).Start();

                    Task.Run(async () =>
                    {
                        await Task.Delay(DefaultPingTimeOut);

                        if (!tcs.Task.IsCompleted)
                            tcs.TrySetResult(null);
                    });

                    var sockaddr = await tcs.Task;

                    if (sockaddr == null)
                        return false;

                    using (var sock = new Socket())
                    {

                        await sock.ConnectAsync(sockaddr, DefaultPingTimeOut);
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to reach: " + host + " Error: " + ex);
                    return false;
                }
            });
        }

		public override void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					ConnectivityChangeBroadcastReceiver.ConnectionChanged = null;
					ConnectivityChangeBroadcastReceiver.ConnectionTypeChanged = null;

					if (connectivityManager != null)
					{
						connectivityManager.Dispose();
						connectivityManager = null;
					}
					try
					{
						Application.Context.UnregisterReceiver(receiver);
					}
					catch (Exception ex)
					{
						DiagnosticLogger.Error($"{ex.ToDiagnosticString()}");
						ex.LogToConsole();
					}
				}

				disposed = true;
			}

			base.Dispose(disposing);
		}

		protected override string GetWiFiConnectionName()
		{
			var wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
			var info = wifiManager.ConnectionInfo;

			return info.SSID;
		}
	}
}
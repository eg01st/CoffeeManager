using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using MobileCore.Extensions;
using MobileCore.Logging;
using SystemConfiguration;

namespace MobileCore.Connection.iOS
{
	public class Connectivity : ConnectivityBase
	{
		private const int ReachabilityChangedDelay = 100;

		private NetworkStatus previousInternetStatus = NetworkStatus.NotReachable;
		private IEnumerable<ConnectionType> previousConnectionTypes = Enumerable.Empty<ConnectionType>();

		private bool disposed;
		private bool isConnected;

		public Connectivity()
		{
			UpdateConnected(false);

			InternetConnectionReachability.ReachabilityChanged += ReachabilityChanged;
		}

		private async void ReachabilityChanged(object sender, EventArgs e)
		{
			//Add in artifical delay so the connection status has time to change
			//else it will return true no matter what.
			await Task.Delay(ReachabilityChangedDelay);

			UpdateConnected();
		}

		private void UpdateConnected(bool triggerChange = true)
		{
			try
			{
				var remoteHostStatus = InternetConnectionReachability.RemoteHostStatus();
				var internetStatus = InternetConnectionReachability.InternetConnectionStatus();

				var previouslyConnected = isConnected;
				isConnected = internetStatus == NetworkStatus.ReachableViaCarrierDataNetwork ||
							  internetStatus == NetworkStatus.ReachableViaWiFiNetwork ||
							  remoteHostStatus == NetworkStatus.ReachableViaCarrierDataNetwork ||
							  remoteHostStatus == NetworkStatus.ReachableViaWiFiNetwork;

				if (triggerChange)
				{
					if (previouslyConnected != isConnected || previousInternetStatus != internetStatus)
					{
						var connectivityEventArgs = new ConnectivityChangedEventArgs
						{
							IsConnected = isConnected
						};

						OnConnectivityChanged(connectivityEventArgs);
					}

					var connectionTypes = ConnectionTypes.ToArray();
					if (!previousConnectionTypes.SequenceEqual(connectionTypes))
					{
						var connectivityTypeChangedEventArgs = new ConnectivityTypeChangedEventArgs
						{
							IsConnected = isConnected,
							ConnectionTypes = connectionTypes
						};

						OnConnectivityTypeChanged(connectivityTypeChangedEventArgs);

						previousConnectionTypes = connectionTypes;
					}
				}

				previousInternetStatus = internetStatus;
			}
			catch (Exception ex)
			{
				var newException = new ConnectivityException("Exception, while updaating connectivity status.", ex);
				DiagnosticLogger.Error($"Error ocurred when tried to update internet connection status, with exception: \n {newException.ToDiagnosticString()}");
				throw newException;
			}
		}

		protected override bool DoGetConnectionStatus()
		{
			return isConnected;
		}

		/// <summary>
		/// Check particular host reachability
		/// <param name="hostUrl"></param>
		/// </summary>
		public override bool CheckHostReachability(string hostUrl)
		{
			if (!IsConnected)
			{
				return false;
			}

			var status = InternetConnectionReachability.InternetConnectionStatus();
			if (status != NetworkStatus.ReachableViaWiFiNetwork && status != NetworkStatus.ReachableViaCarrierDataNetwork)
			{
				return false;
			}

			var isHostReachable = InternetConnectionReachability.IsHostReachable(hostUrl);
			return isHostReachable;
		}

		/// <summary>
		/// Gets the list of all active connection types.
		/// </summary>
		public override IEnumerable<ConnectionType> ConnectionTypes
		{
			get
			{
				var statuses = InternetConnectionReachability.GetActiveConnectionType();
				foreach (var status in statuses)
				{
					switch (status)
					{
						case NetworkStatus.ReachableViaCarrierDataNetwork:
							yield return ConnectionType.Cellular;
							break;
						case NetworkStatus.ReachableViaWiFiNetwork:
							yield return ConnectionType.WiFi;
							break;
						default:
							yield return ConnectionType.Other;
							break;
					}
				}
			}
		}

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="disposing"></param>
		public override void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					InternetConnectionReachability.ReachabilityChanged -= ReachabilityChanged;
					InternetConnectionReachability.Dispose();
				}

				disposed = true;
			}

			base.Dispose(disposing);
		}

		protected override string GetWiFiConnectionName()
		{
			string[] supportedInterfaces;
			CaptiveNetwork.TryGetSupportedInterfaces(out supportedInterfaces);

			if (supportedInterfaces.IsNullOrEmpty())
			{
				return string.Empty;
			}

			NSDictionary currentInfo;
			CaptiveNetwork.TryCopyCurrentNetworkInfo(supportedInterfaces.First(), out currentInfo);

			if (currentInfo.IsNullOrEmpty())
			{
				return string.Empty;
			}

			var ssid = currentInfo.ValueForKey(new NSString("SSID"));

			return ssid.IsNull() ? string.Empty : ssid.ToString();
		}
	}
}
using System;
using System.Net;
using SystemConfiguration;
using CoreFoundation;
using System.Collections.Generic;

namespace MobileCore.Connection.iOS
{
	internal enum NetworkStatus
	{
		NotReachable,
		ReachableViaCarrierDataNetwork,
		ReachableViaWiFiNetwork
	}

	internal static class InternetConnectionReachability
	{
		public static string HostName = "www.google.com";

		// TODO magic numbers
		private static readonly IPAddress AdHocIpAddress = new IPAddress(new byte[] { 169, 254, 0, 0 });

		private static NetworkReachability adHocWiFiNetworkReachability;

		private static NetworkReachability remoteHostReachability;

		private static NetworkReachability defaultRouteReachability;

		//
		// Raised every time there is an interesting reachable event,
		// we do not even pass the info as to what changed, and
		// we lump all three status we probe into one
		//
		public static event EventHandler ReachabilityChanged;

		public static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
		{
			// TODO magic number
			// Is it reachable with the current network configuration?
			var isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

			// TODO magic number
			// Do we need a connection to reach it?
			var noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

			// TODO magic number
			// Since the network stack will automatically try to get the WAN up,
			// probe that
			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				noConnectionRequired = true;
			}

			return isReachable && noConnectionRequired;
		}

		// Is the host reachable with the current network configuration
		public static bool IsHostReachable(string host)
		{
			if (string.IsNullOrEmpty(host))
			{
				return false;
			}

			using (var r = new NetworkReachability(host))
			{
				NetworkReachabilityFlags flags;
				if (r.TryGetFlags(out flags))
				{
					return IsReachableWithoutRequiringConnection(flags);
				}
			}

			return false;
		}

		private static void OnChange(NetworkReachabilityFlags flags)
		{
			ReachabilityChanged?.Invoke(null, EventArgs.Empty);
		}

		public static bool IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (adHocWiFiNetworkReachability == null)
			{
				adHocWiFiNetworkReachability = new NetworkReachability(AdHocIpAddress);
				adHocWiFiNetworkReachability.SetNotification(OnChange);
				adHocWiFiNetworkReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			// TODO check using of out parameter
			var hasFlags = adHocWiFiNetworkReachability.TryGetFlags(out flags);

			// this call looks suspicious after using of out flags in adHocWiFiNetworkReachability.TryGetFlags
			var isReachable = IsReachableWithoutRequiringConnection(flags);

			return hasFlags && isReachable;
		}

		private static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (defaultRouteReachability == null)
			{
				defaultRouteReachability = new NetworkReachability(new IPAddress(0));
				defaultRouteReachability.SetNotification(OnChange);
				defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			// TODO check using of out parameter
			var hasFlags = defaultRouteReachability.TryGetFlags(out flags);

			// this call looks suspicious after using of out flags in adHocWiFiNetworkReachability.TryGetFlags
			var isReachable = IsReachableWithoutRequiringConnection(flags);

			return hasFlags && isReachable;
		}

		public static NetworkStatus RemoteHostStatus()
		{
			// TODO using of flag swith out parameter looks suspicious
			NetworkReachabilityFlags flags;
			bool reachable;

			if (remoteHostReachability == null)
			{
				remoteHostReachability = new NetworkReachability(HostName);

				// Need to probe before we queue, or we wont get any meaningful values
				// this only happens when you create NetworkReachability from a hostname
				reachable = remoteHostReachability.TryGetFlags(out flags);

				remoteHostReachability.SetNotification(OnChange);
				remoteHostReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}
			else
			{
				reachable = remoteHostReachability.TryGetFlags(out flags);
			}

			if (reachable == false)
			{
				return NetworkStatus.NotReachable;
			}

			if (IsReachableWithoutRequiringConnection(flags) == false)
			{
				return NetworkStatus.NotReachable;
			}

			// TODO make more explicit
			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				return NetworkStatus.ReachableViaCarrierDataNetwork;
			}

			return NetworkStatus.ReachableViaWiFiNetwork;
		}

		public static NetworkStatus InternetConnectionStatus()
		{
			NetworkReachabilityFlags flags;
			var defaultNetworkAvailable = IsNetworkAvailable(out flags);

			// TODO make magic numbers more explicit
			if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
			{
				return NetworkStatus.NotReachable;
			}

			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				return NetworkStatus.ReachableViaCarrierDataNetwork;
			}

			if (flags == 0)
			{
				return NetworkStatus.NotReachable;
			}

			return NetworkStatus.ReachableViaWiFiNetwork;
		}

		public static NetworkStatus LocalWifiConnectionStatus()
		{
			NetworkReachabilityFlags flags;
			if (!IsAdHocWiFiNetworkAvailable(out flags))
			{
				return NetworkStatus.NotReachable;
			}

			// TODO magic numbers...
			if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
			{
				return NetworkStatus.ReachableViaWiFiNetwork;
			}

			return NetworkStatus.NotReachable;
		}

		/// <summary>
		/// Checks internet connection status
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<NetworkStatus> GetActiveConnectionType()
		{
			var status = new List<NetworkStatus>();
			NetworkReachabilityFlags flags;

			var defaultNetworkAvailable = IsNetworkAvailable(out flags);
			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
			{
				status.Add(NetworkStatus.ReachableViaCarrierDataNetwork);
			}
			else if (defaultNetworkAvailable)
			{
				status.Add(NetworkStatus.ReachableViaWiFiNetwork);
			}
			else if (((flags & NetworkReachabilityFlags.ConnectionOnDemand) != 0 || (flags & NetworkReachabilityFlags.ConnectionOnTraffic) != 0) && (flags & NetworkReachabilityFlags.InterventionRequired) == 0)
			{
				// If the connection is on-demand or on-traffic and no user intervention
				// is required, then assume WiFi.
				status.Add(NetworkStatus.ReachableViaWiFiNetwork);
			}

			return status;
		}

		/// <summary>
		/// Dispose
		/// </summary>
		public static void Dispose()
		{
			if (remoteHostReachability != null)
			{
				remoteHostReachability.Dispose();
				remoteHostReachability = null;
			}

			if (defaultRouteReachability != null)
			{
				defaultRouteReachability.Dispose();
				defaultRouteReachability = null;
			}

			if (adHocWiFiNetworkReachability != null)
			{
				adHocWiFiNetworkReachability.Dispose();
				adHocWiFiNetworkReachability = null;
			}
		}
	}
}

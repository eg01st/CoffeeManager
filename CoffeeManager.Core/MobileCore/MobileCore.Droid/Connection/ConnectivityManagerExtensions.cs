using System;
using System.Collections.Generic;
using System.Linq;
using Android.Net;
using MobileCore.Extensions;
using MobileCore.Logging;

namespace MobileCore.Connection.Droid
{
	public static class ConnectivityManagerExtensions
	{
		public static bool HasInternetConnection(this ConnectivityManager manager)
		{
			try
			{
				//When on API 21+ need to use getAllNetworks, else fall base to GetAllNetworkInfo
				//https://developer.android.com/reference/android/net/ConnectivityManager.html#getAllNetworks()
				if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
				{
					foreach (var network in manager.GetAllNetworks())
					{
						try
						{
							var info = manager.GetNetworkInfo(network);
							if (info == null)
							{
								continue;
							}

							if (info.IsConnected)
							{
								return true;
							}
						}
						catch
						{
							//there is a possibility, but don't worry
						}
					}
				}
				else
				{
					var allInfo = manager.GetAllNetworkInfo();
					var isConnected = allInfo
						.Where(info => info.IsNotNull())
						.Any(info => info.IsConnected);

					return isConnected;
				}

				return false;
			}
			catch (Exception e)
			{
				DiagnosticLogger.Error($"Error ocurred when tried to check internet connection, with exception: \n {e.ToDiagnosticString()}");
				e.LogToConsole();
				return false;
			}
		}

		public static IEnumerable<ConnectionType> GetConnectionTypes(this ConnectivityManager manager)
		{
			//When on API 21+ need to use getAllNetworks, else fall base to GetAllNetworkInfo
			//https://developer.android.com/reference/android/net/ConnectivityManager.html#getAllNetworks()
			if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
			{
				foreach (var network in manager.GetAllNetworks())
				{
					NetworkInfo info = null;
					try
					{
						info = manager.GetNetworkInfo(network);
					}
					catch
					{
						//there is a possibility, but don't worry about it
					}

					if (info == null || !info.IsAvailable)
					{
						continue;
					}

					yield return GetConnectionType(info.Type);
				}
			}
			else
			{
				foreach (var info in manager.GetAllNetworkInfo())
				{
					if (info == null || !info.IsAvailable)
					{
						continue;
					}

					yield return GetConnectionType(info.Type);
				}
			}
		}

		private static ConnectionType GetConnectionType(ConnectivityType connectivityType)
		{
			switch (connectivityType)
			{
				case ConnectivityType.Ethernet:
					return ConnectionType.Desktop;	
				case ConnectivityType.Wifi:
					return ConnectionType.WiFi;
				case ConnectivityType.Bluetooth:
					return ConnectionType.Bluetooth;
				case ConnectivityType.Mobile:
				case ConnectivityType.MobileDun:
				case ConnectivityType.MobileHipri:
				case ConnectivityType.MobileMms:
					return ConnectionType.Cellular;
				default:
					return ConnectionType.Other;
			}
		}
	}
}
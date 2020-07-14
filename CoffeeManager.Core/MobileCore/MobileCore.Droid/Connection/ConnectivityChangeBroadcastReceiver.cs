using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;

namespace MobileCore.Connection.Droid
{
	/// <summary>
	/// Broadcast receiver to get notifications from Android on connectivity change
	/// </summary>
	[BroadcastReceiver(Enabled = true, Label = "Connectivity Plugin Broadcast Receiver")]
	[Android.Runtime.Preserve(AllMembers = true)]
	public class ConnectivityChangeBroadcastReceiver : BroadcastReceiver
	{
		private const int ConnectivityBroadcastDelay = 500;

		/// <summary>
		/// Action to call when connetivity changes
		/// </summary>
		public static Action<ConnectivityChangedEventArgs> ConnectionChanged;

		/// <summary>
		/// Action to call when connetivity type changes
		/// </summary>
		public static Action<ConnectivityTypeChangedEventArgs> ConnectionTypeChanged;

		private bool isConnected;

		private ConnectionType[] connectionTypes;

		private ConnectivityManager connectivityManager;

		/// <summary>
		/// 
		/// </summary>
		public ConnectivityChangeBroadcastReceiver()
		{
			isConnected = IsConnected;
			connectionTypes = ConnectionTypes.ToArray();
		}

		protected ConnectivityManager ConnectivityManager
		{
			get
			{
				if (connectivityManager == null || connectivityManager.Handle == IntPtr.Zero)
				{
					connectivityManager = (ConnectivityManager)(Application.Context.GetSystemService(Context.ConnectivityService));
				}

				return connectivityManager;
			}
		}

		/// <summary>
		/// Gets if there is an active internet connection
		/// </summary>
		public bool IsConnected => ConnectivityManager.HasInternetConnection();

		/// <summary>
		/// Gets connection types of ConnectivityManager
		/// </summary>
		protected IEnumerable<ConnectionType> ConnectionTypes => ConnectivityManager.GetConnectionTypes();

		/// <summary>
		/// Received a notification via BR.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="intent"></param>
		public override async void OnReceive(Context context, Intent intent)
		{
			if (intent.Action != ConnectivityManager.ConnectivityAction)
			{
				return;
			}

			//await dealy to ensure that the the connection manager updates
			await Task.Delay(ConnectivityBroadcastDelay);

			var newConnectionStatus = CheckConnectionChanged();
			CheckConnectionTypeChanged(newConnectionStatus);
		}

		protected void CheckConnectionTypeChanged(bool newConnectionStatus)
		{
			var connectionTypeChangedAction = ConnectionTypeChanged;
			if (connectionTypeChangedAction == null)
			{
				return;
			}

			var newConnectionTypes = ConnectionTypes.ToArray();
			if (newConnectionTypes.SequenceEqual(connectionTypes))
			{
				return;
			}

			connectionTypes = newConnectionTypes;
			var args = new ConnectivityTypeChangedEventArgs
			{
				IsConnected = newConnectionStatus,
				ConnectionTypes = connectionTypes
			};

			connectionTypeChangedAction(args);
		}

		protected bool CheckConnectionChanged()
		{
			var newConnection = IsConnected;
			var connectionChangedAction = ConnectionChanged;

			if (connectionChangedAction == null)
			{
				return newConnection;
			}

			if (newConnection == isConnected)
			{
				return newConnection;
			}

			isConnected = newConnection;

			var args = new ConnectivityChangedEventArgs
			{
				IsConnected = isConnected
			};

			connectionChangedAction(args);

			return newConnection;
		}
	}
}
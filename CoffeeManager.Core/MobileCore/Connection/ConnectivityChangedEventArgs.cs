using System;

namespace MobileCore.Connection
{
	/// <summary>
	/// Arguments to pass to event handlers
	/// </summary>
	public class ConnectivityChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets if there is an active internet connection
		/// </summary>
		public bool IsConnected { get; set; }
	}
}

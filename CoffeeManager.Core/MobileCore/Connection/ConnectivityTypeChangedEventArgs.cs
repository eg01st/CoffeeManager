using System;
using System.Collections.Generic;

namespace MobileCore.Connection
{
	/// <summary>
	/// Arguments to pass to connectivity type changed event handlers
	/// </summary>
	public class ConnectivityTypeChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets if there is an active internet connection
		/// </summary>
		public bool IsConnected { get; set; }

		/// <summary>
		/// Gets the list of all active connection types.
		/// </summary>
		public IEnumerable<ConnectionType> ConnectionTypes { get; set; }
	}
}

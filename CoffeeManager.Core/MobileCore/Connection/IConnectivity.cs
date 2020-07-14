using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileCore.Connection
{
	/// <summary>
	/// Connectivity changed event handlers
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);

	/// <summary>
	/// Connectivity type changed event handlers
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ConnectivityTypeChangedEventHandler(object sender, ConnectivityTypeChangedEventArgs e);

	public interface IConnectivity
	{
		/// <summary>
		/// Event handler when connection changes
		/// </summary>
		event ConnectivityChangedEventHandler ConnectivityChanged;

		/// <summary>
		/// Event handler when connection type changes
		/// </summary>
		event ConnectivityTypeChangedEventHandler ConnectivityTypeChanged;

		/// <summary>
		/// Gets the list of all active connection types
		/// </summary>
		IEnumerable<ConnectionType> ConnectionTypes { get; }

		/// <summary>
		/// Check weather device is connected or not
		/// </summary>
		bool IsConnected { get; }

		/// <summary>
		/// Gets the current connection status
		/// </summary>
		bool HasInternetConnection { get; }

        Task<bool> HasInternetConnectionAsync { get; }

		/// <summary>
		/// Check particular host reachability
		/// <param name="hostUrl"></param>
		/// </summary>
		bool CheckHostReachability(string hostUrl);

		/// <summary>
		/// Check particular host reachability asynchronously
		/// </summary>
		Task<bool> CheckHostReachabilityAsync(string hostUrl);

		/// <summary>
		/// Check default host reachability
		/// </summary>
		bool CheckDefaultHostReachability();

		/// <summary>
		/// Check default host reachability asynchronously
		/// </summary>
		Task<bool> CheckDefaultHostReachabilityAsync();

		/// <summary>
		/// Throws a <see cref="NoInternetConnectionException"/> if no internet connection detected
		/// </summary>
		void ThrowIfNoInternetConnection();

		/// <summary>
		/// Gets the name of the wi fi network device is connected on.
		/// </summary>
		/// <value>The name of the wi fi connection.</value>
		string WiFiConnectionName { get; }
	}
}
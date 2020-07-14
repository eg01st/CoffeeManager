namespace MobileCore.Connection
{
	/// <summary>
	/// Type of connection
	/// </summary>
	public enum ConnectionType
	{
		/// <summary>
		/// Cellular connection, 3G, Edge, 4G, LTE
		/// </summary>
		Cellular,
		/// <summary>
		/// Wifi connection
		/// </summary>
		WiFi,
		/// <summary>
		/// Desktop or ethernet connection
		/// </summary>
		Desktop,
		/// <summary>
		/// Other type of connection
		/// </summary>
		Other,
		/// <summary>
		/// Bluetooth connection
		/// </summary>
		Bluetooth
	}
}
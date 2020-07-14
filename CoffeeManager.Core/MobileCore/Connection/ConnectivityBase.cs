using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileCore.Logging;

namespace MobileCore.Connection
{
	public abstract class ConnectivityBase : IConnectivity, IDisposable
	{
		protected static string DefaultHostName = "www.google.com";

		private bool disposed;

		/// <summary>
		/// Check weather device is connected or not
		/// </summary>
		public bool IsConnected
		{
			get
			{
				try
				{
					return DoGetConnectionStatus();
				}
				catch (ConnectivityException ex)
				{
					DiagnosticLogger.Error(ex.ToDiagnosticString());
					throw;
				}
				catch (Exception ex)
				{
					var newException = new ConnectivityException("Error while retreiving connectivity status, see inner exception for details", ex);
					DiagnosticLogger.Error(newException.ToDiagnosticString());
					throw newException;
				}
			}
		}

		/// <summary>
		/// Gets the current connection status
		/// </summary>
		public virtual bool HasInternetConnection => CheckDefaultHostReachability();

        public virtual Task<bool> HasInternetConnectionAsync => CheckDefaultHostReachabilityAsync();

		/// <summary>
		/// Check default host reachability
		/// </summary>
		public bool CheckDefaultHostReachability() => CheckHostReachability(DefaultHostName);

		/// <summary>
		/// Check default host reachability asynchronously
		/// </summary>
		public virtual Task<bool> CheckDefaultHostReachabilityAsync() 
			=> CheckHostReachabilityAsync(DefaultHostName);

		/// <summary>
		/// Check particular host reachability
		/// </summary>
		public abstract bool CheckHostReachability(string hostUrl);

		/// <summary>
		/// Check particular host reachability asynchronously
		/// </summary>
		public virtual Task<bool> CheckHostReachabilityAsync(string hostUrl)
		{
			return Task.Run(() => CheckHostReachability(hostUrl));
		}

		/// <summary>
		/// Event, which occures when connectivity changes. 
		/// </summary>
		public event ConnectivityChangedEventHandler ConnectivityChanged;

		/// <summary>
		/// Event, which occures when connection type chagse <see cref="ConnectionTypes"/>
		/// </summary>
		public event ConnectivityTypeChangedEventHandler ConnectivityTypeChanged;

		/// <summary>
		/// When connectivity changes
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnConnectivityChanged(ConnectivityChangedEventArgs e) => ConnectivityChanged?.Invoke(this, e);

		/// <summary>
		/// When connectivity type changes <see cref="ConnectionTypes"/>
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnConnectivityTypeChanged(ConnectivityTypeChangedEventArgs e) => ConnectivityTypeChanged?.Invoke(this, e);

		/// <summary>
		/// Gets the list of all active connection types.
		/// </summary>
		public abstract IEnumerable<ConnectionType> ConnectionTypes { get; }

		public string WiFiConnectionName
		{
			get
			{
				if(!IsConnected)
				{
					return string.Empty;
				}

				var networkName = GetWiFiConnectionName();
				return networkName;
			}
		}

		/// <summary>
		/// Throws a <see cref="NoInternetConnectionException"/> if no internet connection detected
		/// </summary>
		public void ThrowIfNoInternetConnection()
		{
			var hasInternetConnection = IsConnected && HasInternetConnection;
			if (!hasInternetConnection)
			{
				var ex = new NoInternetConnectionException();
				DiagnosticLogger.Error(ex.ToDiagnosticString());
				throw ex;
			}
		}

		protected abstract bool DoGetConnectionStatus();

		protected abstract string GetWiFiConnectionName();

		/// <summary>
		/// Dispose of class and parent classes
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose up
		/// </summary>
		~ConnectivityBase()
		{
			Dispose(false);
		}

		/// <summary>
		/// Dispose method
		/// </summary>
		/// <param name="disposing"></param>
		public virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				//dispose only
			}

			disposed = true;
		}
	}
}
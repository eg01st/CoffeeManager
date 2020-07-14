using System;

namespace MobileCore.Connection
{
    public class NoInternetConnectionException : BusinessDomainException
	{
		public const string DefaultNoInternetConnectionMessage = "No internet connection";

		public NoInternetConnectionException() : base(DefaultNoInternetConnectionMessage) { }

		public NoInternetConnectionException(string message) : base(message) { }

		public NoInternetConnectionException(string message, Exception innerException) : base(message, innerException) { }
	}
}
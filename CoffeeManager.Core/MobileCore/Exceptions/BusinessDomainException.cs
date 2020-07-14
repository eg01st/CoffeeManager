using System;
namespace MobileCore
{
    public abstract class BusinessDomainException : Exception
    {
        protected BusinessDomainException()
        {
        }

        protected BusinessDomainException(string message)
            : base(message)
        {
        }

        protected BusinessDomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public virtual string BuildExceptionString()
        {
            return string.Empty;
        }
    }
}

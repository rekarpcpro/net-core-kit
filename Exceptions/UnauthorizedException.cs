using Xeptions;

namespace NetCoreKit.Exceptions;

public class UnauthorizedException : Xeption
{
	public UnauthorizedException(string message) : base(message)
	{
	}

	public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
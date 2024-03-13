using Xeptions;

namespace NetCoreKit.Exceptions;

public class ForbiddenException : Xeption
{
	public ForbiddenException(string message) : base(message)
	{
	}

	public ForbiddenException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
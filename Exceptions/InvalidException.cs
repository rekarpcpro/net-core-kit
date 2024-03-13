using Xeptions;

namespace NetCoreKit.Exceptions;

public class InvalidException : Xeption
{
	public InvalidException(string message) : base(message)
	{
	}

	public InvalidException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
using Xeptions;

namespace NetCoreKit.Exceptions;

public class InternalServerErrorException : Xeption
{
	public InternalServerErrorException(string message) : base(message)
	{
	}

	public InternalServerErrorException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
using Xeptions;

namespace NetCoreKit.Exceptions;

public class NotFoundException : Xeption
{
	public NotFoundException(string message) : base(message)
	{
	}

	public NotFoundException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
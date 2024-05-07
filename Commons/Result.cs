namespace NetCoreKit.Commons;

#nullable disable

/// <summary>
/// Represents a result of an operation that can either succeed or fail with an <see cref="Exception"/>.
/// </summary>
public class Result
{
	/// <summary>
	/// Gets the exception if the operation failed, otherwise null.
	/// </summary>
	public Exception Exception { get; } = null!;

	/// <summary>
	/// Gets a value indicating whether the operation was successful.
	/// </summary>
	public bool IsSuccess => Exception is null;

#nullable enable

	/// <summary>
	/// Gets a value indicating whether the operation failed.
	/// </summary>
	public bool IsFailure => !IsSuccess;

	/// <summary>
	/// Initializes a new instance of the <see cref="Result"/> class.
	/// </summary>
	public Result()
	{
	}

	private Result(Exception exception)
	{
		Exception = exception;
	}

	/// <summary>
	/// Implicitly converts a boolean value to a successful result.
	/// </summary>
	/// <param name="_">The boolean value that has not purpose other than simplifying returning from successful operation.</param>
	/// <returns>A new instance of <see cref="Result"/>.</returns>
	public static implicit operator Result(bool _)
	{
		return new Result();
	}

	/// <summary>
	/// Implicitly converts an <see cref="Exception"/> to a failed result.
	/// </summary>
	public static implicit operator Result(Exception exception)
	{
		return new Result(exception);
	}

	/// <summary>
	/// Switches on the result, invoking the appropriate action based on whether the operation was successful or failed.
	/// </summary>
	/// <param name="onSuccess">The action to invoke if the operation was successful.</param>
	/// <param name="onFailure">The action to invoke if the operation failed.</param>
	public void Switch(Action onSuccess, Action<Exception> onFailure)
	{
		if (IsSuccess)
		{
			onSuccess();
		}
		else
		{
			onFailure(Exception);
		}
	}

	/// <summary>
	/// Matches the result, invoking the appropriate function based on whether the operation was successful or failed
	/// and returning the something.
	/// </summary>
	/// <param name="onSuccess">The function to invoke if the operation was successful.</param>
	/// <param name="onFailure">The function to invoke if the operation failed.</param>
	public T Match<T>(Func<T> onSuccess, Func<Exception, T> onFailure)
	{
		return IsSuccess ? onSuccess() : onFailure(Exception);
	}
}
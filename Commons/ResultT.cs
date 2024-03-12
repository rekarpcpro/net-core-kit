namespace NetCoreKit.Commons;

#nullable disable

/// <summary>
/// Represents a result of an operation that can either succeed with a value of type <typeparamref name="T"/> or fail with an <see cref="Exception"/>.
/// </summary>
/// <typeparam name="T">The type of the result's value.</typeparam>
public class Result<T>
{
	/// <summary>
	/// Gets the exception if the operation failed, otherwise null.
	/// </summary>
	public Exception Exception { get; set; } = null!;

	/// <summary>
	/// Gets the value if the operation succeeded.
	/// </summary>
	public T Value { get; set; } = default!;

	/// <summary>
	/// Gets a value indicating whether the operation was successful.
	/// </summary>
	public bool IsSuccess => Exception is null;

#nullable enable

	/// <summary>
	/// Gets a value indicating whether the operation failed.
	/// </summary>
	public bool IsFailure => !IsSuccess;

	private Result(T value)
	{
		Value = value;
	}

	private Result(Exception exception)
	{
		Exception = exception;
	}

	/// <summary>
	/// Implicitly converts a value of type <typeparamref name="T"/> to a successful result.
	/// </summary>
	public static implicit operator Result<T>(T data)
	{
		return new Result<T>(data);
	}

	/// <summary>
	/// Implicitly converts an <see cref="Exception"/> to a failed result.
	/// </summary>
	public static implicit operator Result<T>(Exception exception)
	{
		return new Result<T>(exception);
	}

	/// <summary>
	/// Returns the data if the operation was successful, otherwise returns default(T).
	/// </summary>
	public T? Unwrap()
	{
		return this.IsSuccess ? this.Value : default(T?);
	}

	/// <summary>
	/// Matches the result, invoking the appropriate action based on whether the operation was successful or failed.
	/// </summary>
	/// <param name="onSuccess">The action to invoke if the operation was successful.</param>
	/// <param name="onFailure">The action to invoke if the operation failed.</param>
	public void Match(Action<T> onSuccess, Action<Exception> onFailure)
	{
		if (IsSuccess)
		{
			onSuccess(Value);
		}
		else
		{
			onFailure(Exception);
		}
	}
}
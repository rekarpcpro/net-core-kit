using NetCoreKit.Commons;
using NetCoreKit.Exceptions;

namespace NetCoreKit.Extensions;

/// <summary>
/// Extension methods for <see cref="Task{T}"/>.
/// </summary>
public static class ResultExtensions
{
	/// <summary>
	/// Throws an exception if the <see cref="Result"/> is a failure.
	/// </summary>
	/// <param name="result">The result to check.</param>
	public static void ThrowIfFailure(this Result result)
	{
		if (result.IsFailure)
		{
			throw result.Exception;
		}
	}
	/// <summary>
	/// Throws an exception if the <see cref="Result{T}"/> is a failure.
	/// </summary>
	/// <param name="result">The result to check.</param>
	public static void ThrowIfFailure<T>(this Result<T> result)
	{
		if (result.IsFailure)
		{
			throw result.Exception;
		}
	}
}
using NetCoreKit.Exceptions;

namespace NetCoreKit.Extensions;

/// <summary>
/// Extension methods for <see cref="Task{T}"/>.
/// </summary>
public static class TaskExtensions
{
	/// <summary>
	/// Throws an exception if the task's result is null.
	/// </summary>
	/// <typeparam name="T">The type of the task's result.</typeparam>
	/// <param name="task">The task to await.</param>
	/// <param name="message">The optional message for the exception. If null, a default message is used.</param>
	/// <returns>The task's result if it is not null.</returns>
	/// <exception cref="Exception">Thrown when the task's result is null.</exception>
	public static async Task<T> ThrowIfResultNull<T>(this Task<T?> task, string? message = null)
	{
		T? result = await task;

		if (result == null)
		{
			throw new NotFoundException(message ?? $"{typeof(T).Name} is null");
		}

		return result;
	}

	/// <summary>
	/// Throws an exception if the task's result is null, including provided inputs in the exception message.
	/// </summary>
	/// <typeparam name="T">The type of the task's result.</typeparam>
	/// <param name="task">The task to await.</param>
	/// <param name="inputs">The inputs to include in the exception message if the task's result is null.</param>
	/// <returns>The task's result if it is not null.</returns>
	/// <exception cref="Exception">Thrown when the task's result is null, with a message that includes the provided inputs.</exception>
	public static async Task<T> ThrowIfResultNull<T>(this Task<T?> task, params object[] inputs)
	{
		T? result = await task;

		if (result == null)
		{
			throw new Exception(
				string.Format($"{typeof(T).Name} is null, inputs:", string.Join(separator: ", ", inputs))
			);
		}

		return result;
	}
}
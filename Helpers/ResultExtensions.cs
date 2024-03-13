using Microsoft.AspNetCore.Mvc;
using NetCoreKit.Commons;
using NetCoreKit.Exceptions;

namespace NetCoreKit.Helpers;

/// <summary>
/// Provides extension methods for <see cref="Result{T}"/> to easily convert to IActionResult types based on success or failure.
/// </summary>
public static class ResultExtensions
{
	/// <summary>
	/// Converts a <see cref="Result{T}"/> to an OkObjectResult if the operation was successful, otherwise returns an error response.
	/// </summary>
	/// <typeparam name="T">The type of the result's value.</typeparam>
	/// <param name="result">The operation result.</param>
	/// <returns>An IActionResult representing an OK response with the result's value or an error response.</returns>
	public static IActionResult Ok<T>(this Result<T> result)
	{
		return result.IsSuccess ? new OkObjectResult(result.Value) : result.Exception.ReturnErrorResponse();
	}
	
	/// <summary>
	/// Converts a <see cref="Result"/> to an OkObjectResult if the operation was successful, otherwise returns an error response.
	/// </summary>
	/// <param name="result">The operation result.</param>
	/// <returns>An IActionResult representing an OK response or an error response.</returns>
	public static IActionResult Ok(this Result result)
	{
		return result.IsSuccess ? new OkObjectResult(null) : result.Exception.ReturnErrorResponse();
	}

	/// <summary>
	/// Converts a <see cref="Result{T}"/> to a NoContentResult if the operation was successful, otherwise returns an error response.
	/// </summary>
	/// <typeparam name="T">The type of the result's value.</typeparam>
	/// <param name="result">The operation result.</param>
	/// <returns>An IActionResult representing a no content response or an error response.</returns>
	public static IActionResult NoContent<T>(this Result<T> result)
	{
		return result.IsSuccess ? new NoContentResult() : result.Exception.ReturnErrorResponse();
	}
	
	/// <summary>
	/// Converts a <see cref="Result"/> to a NoContentResult if the operation was successful, otherwise returns an error response.
	/// </summary>
	/// <param name="result">The operation result.</param>
	/// <returns>An IActionResult representing a no content response or an error response.</returns>
	public static IActionResult NoContent(this Result result)
	{
		return result.IsSuccess ? new NoContentResult() : result.Exception.ReturnErrorResponse();
	}

	/// <summary>
	/// Returns an error response based on the type of exception contained in the result.
	/// </summary>
	/// <typeparam name="T">The type of the result's value.</typeparam>
	/// <param name="result">The operation result containing the exception.</param>
	/// <returns>An IActionResult representing an error response.</returns>
	public static IActionResult ReturnResponse<T>(this Result<T> result)
	{
		return result.Exception.ReturnErrorResponse();
	}
	
	/// <summary>
	/// Returns an error response based on the type of exception contained in the result.
	/// </summary>
	/// <param name="result">The operation result containing the exception.</param>
	/// <returns>An IActionResult representing an error response.</returns>
	public static IActionResult ReturnResponse(this Result result)
	{
		return result.Exception.ReturnErrorResponse();
	}

	/// <summary>
	/// Determines the appropriate IActionResult to return based on the type of exception.
	/// </summary>
	/// <param name="exception">The exception that occurred.</param>
	/// <returns>An IActionResult representing the specific error based on the exception type.</returns>
	private static IActionResult ReturnErrorResponse(this Exception exception)
	{
		switch (exception)
		{
			case NotFoundException:
			{
				return new ActionResult().NotFound(exception);
			}
			case InvalidException:
			{
				return new ActionResult().BadRequest(exception);
			}
			case ConflictException:
			{
				return new ActionResult().Conflict(exception);
			}
			case UnauthorizedException:
			{
				return new ActionResult().Unauthorized(exception);
			}
			case ForbiddenException:
			{
				return new ActionResult().Forbidden(exception);
			}
			case InternalServerErrorException:
			{
				return new ActionResult().InternalServerError(exception);
			}
			default:
				return new StatusCodeResult(500);
		}
	}
}
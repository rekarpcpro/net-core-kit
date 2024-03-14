using System.Collections;
using System.Dynamic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreKit;

public class ActionResult
{
	private readonly JsonSerializerOptions jsonSerializerOptions = new();

	public ActionResult()
	{
		this.jsonSerializerOptions.DictionaryKeyPolicy ??= this.jsonSerializerOptions.PropertyNamingPolicy;
	}

	public NotFoundObjectResult NotFound(Exception exception)
	{
		var problemDetail = new ValidationProblemDetails
		{
			Status = StatusCodes.Status404NotFound,
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
			Title = exception.Message
		};

		MapExceptionDataToProblemDetail(exception, problemDetail);

		return new NotFoundObjectResult(problemDetail);
	}

	public BadRequestObjectResult BadRequest(Exception exception)
	{
		var problemDetail = new ValidationProblemDetails
		{
			Status = StatusCodes.Status400BadRequest,
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
			Title = exception.Message
		};

		MapExceptionDataToProblemDetail(exception, problemDetail);

		return new BadRequestObjectResult(problemDetail);
	}

	public ConflictObjectResult Conflict(Exception exception)
	{
		var problemDetail = new ValidationProblemDetails
		{
			Status = StatusCodes.Status409Conflict,
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
			Title = exception.Message
		};

		MapExceptionDataToProblemDetail(exception, problemDetail);

		return new ConflictObjectResult(problemDetail);
	}

	public UnauthorizedObjectResult Unauthorized(Exception exception)
	{
		var problemDetail = new ValidationProblemDetails
		{
			Status = StatusCodes.Status401Unauthorized,
			Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
			Title = exception.Message
		};

		MapExceptionDataToProblemDetail(exception, problemDetail);

		return new UnauthorizedObjectResult(problemDetail);
	}

	public ObjectResult Forbidden(Exception exception)
	{
		var problemDetail = new ValidationProblemDetails
		{
			Status = StatusCodes.Status403Forbidden,
			Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
			Title = exception.Message
		};

		MapExceptionDataToProblemDetail(exception, problemDetail);

		return new ObjectResult(problemDetail) { StatusCode = StatusCodes.Status403Forbidden };
	}

	public ObjectResult InternalServerError(Exception exception)
	{
		var problemDetail = new ValidationProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
			Title = exception.Message
		};

		MapExceptionDataToProblemDetail(exception, problemDetail);

		return new ObjectResult(problemDetail) { StatusCode = StatusCodes.Status500InternalServerError };
	}

	private void MapExceptionDataToProblemDetail(Exception exception, ValidationProblemDetails problemDetail)
	{
		foreach (DictionaryEntry error in exception.Data)
		{
			string errorKey = ApplySerialization(error, jsonSerializerOptions);
			problemDetail.Errors.Add(key: errorKey, value: ((List<string>)error.Value!).ToArray());
		}
	}

	private static string ApplySerialization(DictionaryEntry error, JsonSerializerOptions jsonSerializerOptions)
	{
		IDictionary<string, object> interimObject = new ExpandoObject()!;
		interimObject.Add(error.Key.ToString()!, error.Value!);
		string serialisedInterimObject = JsonSerializer.Serialize(interimObject, jsonSerializerOptions);

		var deserializedError =
			JsonSerializer.Deserialize<IDictionary<string, object>>(serialisedInterimObject, jsonSerializerOptions)!;

		return deserializedError.Keys.First();
	}
}
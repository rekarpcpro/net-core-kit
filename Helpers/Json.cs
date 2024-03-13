using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCoreKit.Helpers;

/// <summary>
/// Provides static methods for serializing objects to JSON strings and deserializing JSON strings to objects.
/// Utilizes System.Text.Json for JSON processing with default settings that can be overridden by custom options.
/// </summary>
public static class Json
{
	/// <summary>
	/// Static field to hold the default JsonSerializerOptions.
	/// Includes JsonStringEnumConverter, sets PropertyNameCaseInsensitive to true,
	/// and applies CamelCase naming policy to JSON property names.
	/// </summary>
	private static readonly JsonSerializerOptions defaultOptions = new()
	{
		Converters = { new JsonStringEnumConverter() },
		PropertyNameCaseInsensitive = true,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};

	/// <summary>
	/// Serializes the specified object to a JSON string.
	/// </summary>
	/// <typeparam name="T">The type of the object to serialize.</typeparam>
	/// <param name="obj">The object to serialize.</param>
	/// <param name="options">Optional. The JsonSerializerOptions to use for serialization. If not provided, uses default options.</param>
	/// <returns>A JSON string representation of the object.</returns>
	public static string ToJson<T>(T obj, JsonSerializerOptions? options = null)
	{
		return JsonSerializer.Serialize(obj, options ?? defaultOptions);
	}

	/// <summary>
	/// Deserializes the JSON string to an object of type T.
	/// </summary>
	/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
	/// <param name="json">The JSON string to deserialize.</param>
	/// <param name="options">Optional. The JsonSerializerOptions to use for deserialization. If not provided, uses default options.</param>
	/// <returns>An object of type T representing the deserialized JSON string. Returns null if deserialization fails.</returns>
	public static T? FromJson<T>(string json, JsonSerializerOptions? options = null)
	{
		return JsonSerializer.Deserialize<T>(json, options ?? defaultOptions);
	}
}
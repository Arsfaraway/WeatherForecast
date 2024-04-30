using System.Net;

namespace WeatherReport.Exceptions;

public static class ErrorRegistry
{
    public static WeatherException InternalServerError() =>
        new(HttpStatusCode.InternalServerError, "Internal server error");

    public static WeatherException WeatherApiError(string city) =>
        new(HttpStatusCode.ServiceUnavailable, $"Error while getting weather forecast for city {city}");

    public static WeatherException CityNotFoundError(string city) =>
        new(HttpStatusCode.NotFound, $"City '{city}' not found");
}

using System.Net;

namespace WeatherReport.Exceptions;

public class WeatherException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public WeatherException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
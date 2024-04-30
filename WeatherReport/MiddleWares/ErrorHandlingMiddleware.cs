using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherReport.Exceptions;
using WeatherReport.Models;

namespace WeatherReport.MiddleWares
{

  public class ErrorHandlingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    public ErrorHandlingMiddleware(RequestDelegate next,  ILogger<ErrorHandlingMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      HttpStatusCode code;
      object errorObject;

      switch (exception)
      {
        case WeatherException ex:
          {
            _logger.LogError(exception.Message);
            errorObject = new Error(){ Message = ex.Message};
            code = ex.StatusCode;
            break;
          }
        default:
          {
           _logger.LogError(exception.Message);
            errorObject = new Error(){ Message = exception.Message};
            code = HttpStatusCode.InternalServerError;
            break;
          }
      }

      var result = JsonConvert.SerializeObject(errorObject, Formatting.Indented);
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)code;
      return context.Response.WriteAsync(result);
    }
  }
}
using Microsoft.AspNetCore.Authentication;
using System.Threading;
using WeatherReport.MiddleWares;
using WeatherReport.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IInformService, InformService>();
builder.Services.AddTransient<ILogger<InformService>, Logger<InformService>>();
builder.Services.AddTransient<ILogger<ErrorHandlingMiddleware>, Logger<ErrorHandlingMiddleware>>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
// if (Env.IsDevelopment())
// app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
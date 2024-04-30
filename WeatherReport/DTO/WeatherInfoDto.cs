
using Microsoft.AspNetCore.Authentication;
using WeatherReport.Mapping;
using WeatherReport.Models;
using WeatherReport.Services;

namespace WeatherReport.DTO
{
    public class WeatherInfoDto
    {
        public string? City { get; set; }
        
        public TimeSpan? CityCurrentTime { get; set; }

        public TimeSpan? ServerCurrentTime { get; set; }

        public TimeSpan? TimeDifferenceBetweenCityAndServer { get; set; }

        public int? CelsiusTemperature { get; set; }

        public int? AtmosphericPressure { get; set; }

        public int? AirHumidity { get; set; }

        public double? WindSpeed { get; set; }

        public int? CloudCover { get; set; }
        
        internal static WeatherInfoDto TakeInformationDto(Forecast forecast)
        {
            return new WeatherInfoDto()
            {
                City = forecast.Name,
                CityCurrentTime =  Time.TakeCityCurrentTime(forecast.Timezone),
                ServerCurrentTime =  Time.TakeServerCurrentTime(forecast.Timezone),
                TimeDifferenceBetweenCityAndServer = Time.TakeTimeDifferenceBetweenCityAndServer(forecast.Timezone),
                CelsiusTemperature = (int)Math.Round(forecast.Main.Temp - 273.15),
                AtmosphericPressure = forecast.Main?.Pressure,
                AirHumidity = forecast.Main?.Humidity,
                WindSpeed = Math.Round(forecast.Wind.Speed, 1),
                CloudCover = forecast.Clouds?.All
            };
        }
    }
}


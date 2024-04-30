using WeatherReport.Models;

namespace WeatherReport.DTO;

public class AverageWeatherInfoDto
{
    public int? AverageTemperature { get; set; }
    public WeatherInfoDto? FirstCity { get; set; }
    public WeatherInfoDto? SecondCity { get; set; }
    internal static AverageWeatherInfoDto TakeInformationDto(WeatherInfoDto oneForecast, WeatherInfoDto twoForecast)
    {
        return new AverageWeatherInfoDto()
        {
            AverageTemperature = (oneForecast.CelsiusTemperature + twoForecast.CelsiusTemperature) / 2,
            FirstCity = oneForecast,
            SecondCity = twoForecast
        };
    }
}
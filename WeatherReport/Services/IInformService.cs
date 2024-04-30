using WeatherReport.DTO;
using WeatherReport.Models;

namespace WeatherReport.Services;

public interface IInformService
{
    public Task<WeatherInfoDto> GetForecast(string city);
    
    public string GetXmlForecast(WeatherInfoDto weatherInfoDto);
}
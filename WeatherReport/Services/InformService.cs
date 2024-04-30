using System.Net;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherReport.DTO;
using WeatherReport.Exceptions;
using WeatherReport.Models;

namespace WeatherReport.Services;

public class InformService : IInformService
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<InformService> _logger;

    public InformService(ILogger<InformService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<WeatherInfoDto> GetForecast(string city)
    {
        var locations = new Forecast();
        //todo использую версию 2.5, потом что 3.0 не работает не в одном apiUrl
        string apiUrl =
            $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=" + _configuration["APIKey"];
        
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                locations = JsonConvert.DeserializeObject<Forecast>(responseBody);

                _logger.LogDebug($"City: {locations.Name}");
                _logger.LogDebug($"Timezone: {locations.Timezone}");
                _logger.LogDebug($"Temp: {locations.Main.Temp}");
                _logger.LogDebug($"Pressure: {locations.Main.Pressure}");
                _logger.LogDebug($"Humidity: {locations.Main.Humidity}");
                _logger.LogDebug($"Speed: {locations.Wind.Speed}");
                _logger.LogDebug($"All: {locations.Clouds.All}");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw ErrorRegistry.CityNotFoundError(city);
            }
            else
            {
                throw new HttpRequestException($"HTTP Error: {response.StatusCode}");
            }
        }
        return WeatherInfoDto.TakeInformationDto(locations);
    }

    public string GetXmlForecast(WeatherInfoDto weatherInfoDto)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(WeatherInfoDto));

        try
        {
            using StringWriter stringWriter = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(xmlWriter, weatherInfoDto);
            }

            _logger.LogDebug($"Xml Forecast: {stringWriter.ToString()}");
            return stringWriter.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred during XML serialization: {ex.Message}");
            throw new Exception($"An error occurred during XML serialization: {ex.Message}");
        }
    }
}
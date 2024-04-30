using System.Xml.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WeatherReport.DTO;
using WeatherReport.Exceptions;
using WeatherReport.Models;
using WeatherReport.Services;

namespace WeatherReport.Controllers
{
    [ApiController]
    [Route("[controller]/cities")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IInformService _informService;

        public WeatherForecastController(IInformService informService)
        {
            _informService = informService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeatherForecast([FromRoute] string city)
        {
            var informDto = await _informService.GetForecast(city);

            return new JsonResult(informDto);
        }

        [HttpGet("{city}/xml")]
        public async Task<IActionResult> GetWeatherForecastXml([FromRoute] string city)
        {
            var informDto = await _informService.GetForecast(city);

            return Content(_informService.GetXmlForecast(informDto));
        }

        [HttpGet("{city1}/{city2}/average-temperature")]
        public async Task<IActionResult> GetAverageTempWeatherForecastBetweenCities([FromRoute] string city1,
            [FromRoute] string city2)
        {
            var informDtoFirst = await _informService.GetForecast(city1);

            var informDtoSecond = await _informService.GetForecast(city2);

            return new JsonResult(AverageWeatherInfoDto.TakeInformationDto(informDtoFirst, informDtoSecond));
        }
    }
}
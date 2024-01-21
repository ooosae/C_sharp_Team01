using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using rush01.WeatherClient;

namespace rush01;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherClient.WeatherClient _weatherClient;
    private readonly IMemoryCache _cache;

    public WeatherForecastController(WeatherClient.WeatherClient weatherClient, IMemoryCache cache)
    {
        _weatherClient = weatherClient;
        _cache = cache;
    }

    /// <summary>
    /// Get weather information by city name.
    /// </summary>
    /// <param name="cityName">The name of the city.</param>
    /// <returns>Returns weather information for the specified city.</returns>
    [HttpGet("{cityName}")]
    [ProducesResponseType(typeof(OpenWeatherResponse), 200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IActionResult> GetWeatherByCityName(string? cityName = null)
    {
        string? selectedCity = string.IsNullOrWhiteSpace(cityName) ? _cache.Get<string>("defaultCity") : cityName;

        if (string.IsNullOrWhiteSpace(selectedCity))
        {
            return NotFound("City not specified.");
        }
        
        var result = await _weatherClient.GetWeatherByCityName(selectedCity);

        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Unable to get weather data from OpenWeather.");
        }
    }

    /// <summary>
    /// Get weather information by geographic coordinates.
    /// </summary>
    /// <param name="latitude">The latitude of the location.</param>
    /// <param name="longitude">The longitude of the location.</param>
    /// <returns>Returns weather information for the specified coordinates.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(OpenWeatherResponse), 200)]
    [ProducesResponseType(400)]
    [Produces("application/json")]
    public async Task<IActionResult> Get(double latitude, double longitude)
    {
        var result = await _weatherClient.GetWeatherByCoordinates(latitude, longitude);

        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Unable to get weather data from OpenWeather.");
        }
    }
    
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult Post([FromBody] string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName))
        {
            return BadRequest("City name cannot be empty.");
        }

        _cache.Set("defaultCity", cityName);

        return Ok();
    }
}
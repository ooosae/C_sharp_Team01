using Microsoft.Extensions.Options;

namespace rush01.WeatherClient;

public class WeatherClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;

    public WeatherClient(IHttpClientFactory httpClientFactory, IOptions<ServiceSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        _apiKey = options.Value.ApiKey;
    }
    public async Task<OpenWeatherResponse>? GetWeatherByCityName(string cityName)
    {
        try
        {
            var apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<OpenWeatherResponse>();
                    result.Main.Temp = Math.Round(result.Main.Temp - 273.15, 2);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<OpenWeatherResponse>? GetWeatherByCoordinates(double latitude, double longitude)
    {
        try
        {
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<OpenWeatherResponse>();
                result.Main.Temp = Math.Round(result.Main.Temp - 273.15, 2);
                return result;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

namespace rush01.WeatherClient;

public class OpenWeatherResponse
{
    public Wind Wind { get; set; }
    public List<Weather> Weather { get; set; }
    public Main Main { get; set; }
    public string Name { get; set; }
    
    
}

public class Wind
{
    public double Speed { get; set; }
}

public class Weather
{
    public string Description { get; set; }
}

public class Main
{
    public double Temp  { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
}
using WeatherStation.Info;
using WeatherStation.Observer.Displays.Stats;

namespace WeatherStation.Observer.Displays;

public class StatsDisplayPro : IObserverType<WeatherInfoPro>
{
    private const string _temperatureTitle = "Temperature";
    private const string _pressureTitle = "Pressure";
    private const string _humidityTitle = "Humidity";
    private const string _windSpeedTitle = "Wind speed";

    private WeatherStats _temperatureData = new();
    private WeatherStats _humidityData = new();
    private WeatherStats _pressureData = new();
    private WeatherStats _windSpeedData = new();
    private WindDirectionStats _windDirection = new();

    public void Update( WeatherInfoPro infoPro )
    {
        _temperatureData.Update( infoPro.Temperature );
        _humidityData.Update( infoPro.Humidity );
        _pressureData.Update( infoPro.Pressure );
        _windSpeedData.Update( infoPro.Wind.speed );
        _windDirection.Update( infoPro.Wind.direction );

        DisplayStats();
    }

    private void DisplayStats()
    {
        Console.WriteLine( "=== StatsDisplayPro info ===" );
        PrintStats( _temperatureData, _temperatureTitle );
        PrintStats( _pressureData, _pressureTitle );
        PrintStats( _humidityData, _humidityTitle );
        PrintStats( _windSpeedData, _windSpeedTitle );
        PrintWindDirectionStats();
    }

    private void PrintStats( WeatherStats data, string title )
    {
        var stats = data.GetStats();

        Console.WriteLine( $"=> {title}" );
        Console.WriteLine( $"Min: {stats.min}" );
        Console.WriteLine( $"Max: {stats.max}" );
        Console.WriteLine( $"Average: {stats.average}" );
        Console.WriteLine( "-----------" );
    }

    private void PrintWindDirectionStats()
    {
        Console.WriteLine( "=> Wind direction" );
        Console.WriteLine( $"Average {_windDirection.GetAverageData()}" );
        Console.WriteLine( "-----------" );
    }
}
using WeatherStation.Info;

namespace WeatherStation.Observer.Displays;

public class StatsDisplay : IObserverType<WeatherInfo>
{
    private const string _temperatureTitle = "Temperature";
    private const string _pressureTitle = "Pressure";
    private const string _humidityTitle = "Humidity";

    private WeatherStats _temperatureData = new();
    private WeatherStats _humidityData = new();
    private WeatherStats _pressureData = new();

    public void Update( WeatherInfo info )
    {
        _temperatureData.Update( info.temperature );
        _humidityData.Update( info.humidity );
        _pressureData.Update( info.pressure );

        DisplayStats();
    }

    private void DisplayStats()
    {
        Console.WriteLine( "=== StatsDisplay info ===" );
        PrintStats( _temperatureData, _temperatureTitle );
        PrintStats( _pressureData, _pressureTitle );
        PrintStats( _humidityData, _humidityTitle );
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
}
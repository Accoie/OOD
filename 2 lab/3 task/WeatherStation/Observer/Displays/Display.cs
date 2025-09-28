using WeatherStation.Info;

namespace WeatherStation.Observer.Displays;

public class Display : IObserverType<WeatherInfo>
{
    public void Update( WeatherInfo data )
    {
        Console.WriteLine( "=== StatsDisplay info ===" );
        Console.WriteLine( $"Current temperature: {data.temperature}" );
        Console.WriteLine( $"Current humidity: {data.humidity}" );
        Console.WriteLine( $"Current Pressure: {data.pressure}" );
        Console.WriteLine( "-----------" );
    }
}
using WeatherStation.Info;

namespace WeatherStation.Observer.Displays;

public class Display : IObserverType<WeatherInfo>
{
    public void Update( WeatherInfo data )
    {
        Console.WriteLine( "=== StatsDisplay info ===" );
        Console.WriteLine( $"Current temperature: {data.Temperature}" );
        Console.WriteLine( $"Current humidity: {data.Humidity}" );
        Console.WriteLine( $"Current Pressure: {data.Pressure}" );
        Console.WriteLine( "-----------" );
    }
}
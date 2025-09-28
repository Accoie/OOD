using WeatherStation.Info;

namespace WeatherStation.Observer.Displays;

public class DisplayPro : IObserverType<WeatherInfoPro>
{
    public void Update( WeatherInfoPro data )
    {
        Console.WriteLine( "=== StatsDisplayPro info ===" );
        Console.WriteLine( $"Current temperature: {data.Temperature}" );
        Console.WriteLine( $"Current humidity: {data.Humidity}" );
        Console.WriteLine( $"Current Pressure: {data.Pressure}" );
        Console.WriteLine( $"Current wind speed: {data.Wind.speed}" );
        Console.WriteLine( $"Current wind direction: {data.Wind.direction}" );
        Console.WriteLine( "-----------" );
    }
}
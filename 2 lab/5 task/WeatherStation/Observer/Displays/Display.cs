using WeatherStation.Info;
using WeatherStation.Observable;

namespace WeatherStation.Observer.Displays;

public class Display : IObserverType<WeatherInfo>
{
    public void Update( WeatherInfo data, IObservableType<WeatherInfo> station )
    {
        Console.WriteLine( "=== StatsDisplay info ===" );

        if ( station.GetType() == typeof( WeatherDataIn ) )
        {
            Console.WriteLine( "Inside" );
        }
        if ( station.GetType() == typeof( WeatherDataOut ) )
        {
            Console.WriteLine( "Outside" );
        }

        Console.WriteLine( $"Current temperature: {data.temperature}" );
        Console.WriteLine( $"Current humidity: {data.humidity}" );
        Console.WriteLine( $"Current Pressure: {data.pressure}" );
        Console.WriteLine( $"Current wind speed: {data.wind.speed}" );
        Console.WriteLine( $"Current wind direction: {data.wind.direction}" );
        Console.WriteLine( "-----------" );
    }
}
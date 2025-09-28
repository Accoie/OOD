using WeatherStation.Observable;
using WeatherStation.Observer.Displays;

public class Program
{
    public static void Main()
    {
        WeatherData weatherData = new WeatherData();

        Display display = new Display();
        weatherData.RegisterObserver( display );

        StatsDisplay statsDisplay = new StatsDisplay();
        weatherData.RegisterObserver( statsDisplay );

        weatherData.SetMeasurements( 3, 0.7, 760 );
        weatherData.SetMeasurements( 4, 0.8, 761 );

        weatherData.RemoveObserver( statsDisplay );

        weatherData.SetMeasurements( 10, 0.7, 760 );
        weatherData.SetMeasurements( -10, 0.8, 761 );
    }
}
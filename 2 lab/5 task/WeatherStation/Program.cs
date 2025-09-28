using WeatherStation.Observable;
using WeatherStation.Observer.Displays;

public class Program
{
    public static void Main()
    {
        WeatherDataIn weatherData = new WeatherDataIn();
        WeatherDataIn weatherDataOut = new WeatherDataIn();

        Display display = new Display();
        weatherData.RegisterObserver( display, 0 );

        StatsDisplay statsDisplay = new StatsDisplay();
        weatherData.RegisterObserver( statsDisplay, 0 );

        weatherData.SetMeasurements( 3, 0.7, 760, 4, 144 );
        weatherData.SetMeasurements( 3, 0.7, 760, 4, 270 );
    }
}
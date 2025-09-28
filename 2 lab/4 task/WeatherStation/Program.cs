using WeatherStation.Observable;
using WeatherStation.Observer.Displays;

public class Program
{
    public static void Main()
    {
        WeatherDataIn weatherDataIn = new WeatherDataIn();
        WeatherDataOut weatherDataOut = new WeatherDataOut();

        Display display = new Display();
        weatherDataIn.RegisterObserver( display, 0 );
        weatherDataOut.RegisterObserver( display, 0 );

        StatsDisplay statsDisplay = new StatsDisplay();
        weatherDataIn.RegisterObserver( statsDisplay, 0 );
        weatherDataOut.RegisterObserver( statsDisplay, 0 );

        weatherDataIn.SetMeasurements( 3, 0.7, 760 );
        weatherDataOut.SetMeasurements( 3, 0.7, 760 );
        weatherDataIn.SetMeasurements( 4, 0.8, 761 );
        weatherDataOut.SetMeasurements( 4, 0.8, 761 );

        weatherDataIn.RemoveObserver( statsDisplay );
        weatherDataOut.RemoveObserver( statsDisplay );

        weatherDataIn.SetMeasurements( 10, 0.8, 761 );
        weatherDataOut.SetMeasurements( 10, 0.8, 761 );
        weatherDataIn.SetMeasurements( -10, 0.8, 761 );
        weatherDataOut.SetMeasurements( -10, 0.8, 761 );
    }
}
using WeatherStation.Events;
using WeatherStation.Observable;
using WeatherStation.Observer.Displays;

public class Program
{
    public static void Main()
    {
        WeatherDataPro weatherDataPro = new WeatherDataPro();

        DisplayPro displayPro = new DisplayPro();
        weatherDataPro.RegisterObserver( displayPro, events: [ WeatherDataProEvents.Temperature, WeatherDataProEvents.WindSpeed ], 0 );

        weatherDataPro.SetMeasurements( 3, 0.7, 760, 15, 720 );
        weatherDataPro.SetMeasurements( 3, 0.7, 760, 15, 270 );
        weatherDataPro.SetMeasurements( 3, 0.7, 760, 4, 340 );

        weatherDataPro.RegisterObserver( displayPro, [WeatherDataProEvents.WindDirection], 0 );

        weatherDataPro.SetMeasurements( 3, 0.7, 760, 4, 270 );
    }
}
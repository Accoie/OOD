using WeatherStation.Observable;
using WeatherStation.Observer.Displays;

public class Program
{
    public static void Main()
    {
        WeatherData weatherDataIn = new WeatherData();

        Display display = new Display();
        weatherDataIn.RegisterObserver( display, 0 );

        StatsDisplay statsDisplay = new StatsDisplay();
        weatherDataIn.RegisterObserver( statsDisplay, 0 );

        weatherDataIn.SetMeasurements( 25, 0.65, 1013 );
        weatherDataIn.SetMeasurements( 18, 0.45, 1005 );

        WeatherDataPro weatherDataOut = new WeatherDataPro();

        DisplayPro displayPro = new DisplayPro();
        weatherDataOut.RegisterObserver( displayPro, 0 );

        StatsDisplayPro statsDisplayPro = new StatsDisplayPro();
        weatherDataOut.RegisterObserver( statsDisplayPro, 0 );

        weatherDataOut.SetMeasurements( 30, 0.55, 1020, 25, 180 );
        weatherDataOut.SetMeasurements( 22, 0.35, 1010, 18, 90 );
        weatherDataOut.SetMeasurements( 15, 0.75, 995, 12, 300 );
    }
}
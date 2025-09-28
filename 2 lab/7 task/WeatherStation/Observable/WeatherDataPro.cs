using WeatherStation.Events;
using WeatherStation.Info;

namespace WeatherStation.Observable;

public class WeatherDataPro : Observable<WeatherInfoPro>
{
    public double Temperature { get; private set; } = 0;
    public double Humidity { get; private set; } = 0;
    public double Pressure { get; private set; } = 760;
    public double WindSpeed { get; private set; } = 0;
    public double WindDirection { get; private set; } = 0;

    public void MeasurementsChanged( string[] events )
    {
        NotifyObservers( events );
    }

    public void SetMeasurements( double temperature, double humidity, double pressure, double windSpeed, double windDirection )
    {
        List<string> events = [];

        if ( Temperature != temperature )
        {
            Temperature = temperature;
            events.Add( WeatherDataEvents.Temperature );
        }

        if ( Humidity != humidity )
        {
            Humidity = humidity;
            events.Add( WeatherDataEvents.Humidity );
        }

        if ( Pressure != pressure )
        {
            Pressure = pressure;
            events.Add( WeatherDataEvents.Pressure );
        }

        if ( WindSpeed != windSpeed )
        {
            WindSpeed = windSpeed;
            events.Add( WeatherDataProEvents.WindSpeed );
        }

        if ( WindDirection != windDirection )
        {
            WindDirection = windDirection;
            events.Add( WeatherDataProEvents.WindDirection );
        }

        MeasurementsChanged( events.ToArray() );
    }

    protected override WeatherInfoPro GetChangedData()
    {
        return new WeatherInfoPro
        {
            Humidity = Humidity,
            Pressure = Pressure,
            Temperature = Temperature,
            Wind = new WindInfo()
            {
                speed = WindSpeed,
                direction = WindDirection
            }
        };
    }
}
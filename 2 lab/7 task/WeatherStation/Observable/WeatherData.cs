using WeatherStation.Events;
using WeatherStation.Info;

namespace WeatherStation.Observable;

public class WeatherData : Observable<WeatherInfo>
{
    public double Temperature { get; protected set; } = 0;
    public double Humidity { get; protected set; } = 0;
    public double Pressure { get; protected set; } = 760;

    public void MeasurementsChanged(string[] events)
    {
        NotifyObservers( events);
    }

    public void SetMeasurements( double temperature, double humidity, double pressure )
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

        MeasurementsChanged( events.ToArray() );
    }

    protected override WeatherInfo GetChangedData()
    {
        return new WeatherInfo
        {
            Humidity = Humidity,
            Pressure = Pressure,
            Temperature = Temperature,
        };
    }
}